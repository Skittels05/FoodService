using System.Text.Json;
using DeliveryService.BLL.Models;
using DeliveryService.DAL.Exceptions;
using DeliveryService.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Wolverine;

namespace DeliveryService.DAL.Outbox;

[DisallowConcurrentExecution]
public class OutboxProcessorJob(
    ApplicationDbContext dbContext,
    IMessageBus messageBus,
    IOptions<OutboxOptions> options,
    ILogger<OutboxProcessorJob> logger,
    TimeProvider timeProvider,
    [FromKeyedServices(OutboxOptions.JsonOptionsKey)] JsonSerializerOptions jsonOptions)
    : IJob
{
    public static readonly JobKey Key = new(nameof(OutboxProcessorJob));

    private readonly OutboxOptions _options = options.Value;

    public async ValueTask Execute(IJobExecutionContext context, CancellationToken cancellationToken)
    {
        try
        {
            await ProcessOutboxMessagesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An unhandled exception occurred during Outbox processing cycle.");
        }
    }

    private async Task ProcessOutboxMessagesAsync(CancellationToken cancellationToken)
    {
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
            await ProcessMessageAsync(message);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
    }

    private async Task ProcessMessageAsync(OutboxMessage message)
    {
        try
        {
            var type = Type.GetType(message.Type)
                ?? throw new OutboxTypeNotFoundException(message.Type);

            var deserializedEvent = JsonSerializer.Deserialize(message.Content, type, jsonOptions)
                ?? throw new OutboxDeserializationException(message.Id, message.Type);

            var deliveryOptions = new DeliveryOptions();
            deliveryOptions.WithHeader("EventId", message.Id.ToString());

            await messageBus.PublishAsync(deserializedEvent, deliveryOptions);
            message.ProcessedOn = timeProvider.GetUtcNow();
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
        message.ProcessedOn = timeProvider.GetUtcNow();
        message.RetryCount = _options.MaxRetryCount;
    }
}
