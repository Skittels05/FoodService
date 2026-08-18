namespace DeliveryService.DAL.Outbox;

using System.Text.Json;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Services.Interfaces;
using DeliveryService.DAL.Persistence;

public class OutboxWriter(
    ApplicationDbContext dbContext,
    TimeProvider timeProvider) : IOutboxWriter
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public void Write<TEvent>(TEvent @event) where TEvent : class
    {
        var outboxMessage = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            OccurredOnUtc = timeProvider.GetUtcNow().UtcDateTime,
            Type = @event.GetType().AssemblyQualifiedName!,
            Content = JsonSerializer.Serialize(@event, JsonOptions)
        };

        dbContext.Add(outboxMessage);
    }
}
