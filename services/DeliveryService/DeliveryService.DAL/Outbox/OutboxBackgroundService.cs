using System.Text.Json;
using DeliveryService.BLL.Models;
using DeliveryService.DAL.Exceptions;
using DeliveryService.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wolverine;

namespace DeliveryService.DAL.Outbox;

public class OutboxBackgroundService(
    IServiceScopeFactory scopeFactory,
    IOptions<OutboxOptions> options,
    ILogger<OutboxBackgroundService> logger,
    TimeProvider timeProvider) 
    : BackgroundService
{
    private readonly OutboxOptions _options = options.Value;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(_options.IntervalInSeconds));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await ProcessOutboxMessagesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred during Outbox processing cycle.");
            }
        }
    }

    private async Task ProcessOutboxMessagesAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var messageBus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        var messages = await dbContext.Set<OutboxMessage>()
            .GetPendingForUpdate(_options.BatchSize)
            .ToListAsync(cancellationToken);

        if (messages.Count == 0)
        {
            return; 
        }

        foreach (var message in messages)
        {
            await ProcessMessageAsync(message, messageBus);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    private async Task ProcessMessageAsync(OutboxMessage message, IMessageBus messageBus)
    {
        try
        {
            var type = Type.GetType(message.Type)
                ?? throw new OutboxTypeNotFoundException(message.Type);

            var deserializedEvent = JsonSerializer.Deserialize(message.Content, type, _jsonOptions)
                ?? throw new OutboxDeserializationException(message.Id, message.Type);

            var deliveryOptions = new DeliveryOptions();
            deliveryOptions.WithHeader("EventId", message.Id.ToString());
            
            await messageBus.PublishAsync(deserializedEvent, deliveryOptions);
            message.ProcessedOnUtc = timeProvider.GetUtcNow().UtcDateTime;
            message.Error = null;
        }
        catch (OutboxException ex)
        {
            MarkAsPermanentFailed(message, ex);
        }
        catch (Exception ex)
        {
            message.RetryCount++;
            message.Error = ex.ToString();

            if (message.RetryCount >= _options.MaxRetryCount)
            {
                MarkAsPermanentFailed(message, ex);
                logger.LogCritical(ex, "OutboxMessage {MessageId} reached max retry limit and was marked as permanently failed.", message.Id);
            }
            else
            {
                logger.LogWarning(ex, "Failed to publish OutboxMessage {MessageId}. Retry {RetryCount} of {MaxRetryCount}.", message.Id, message.RetryCount, _options.MaxRetryCount);
            }
        }
    }

    private void MarkAsPermanentFailed(OutboxMessage message, Exception ex)
    {
        logger.LogError(ex, "Permanent error processing OutboxMessage {MessageId}", message.Id);
        message.Error = ex.Message;
        message.ProcessedOnUtc = timeProvider.GetUtcNow().UtcDateTime;
    }
}
