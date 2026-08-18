using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.DAL.Outbox;

using System.Text.Json;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Services.Interfaces;
using DeliveryService.DAL.Persistence;

public class OutboxWriter(
    ApplicationDbContext dbContext,
    TimeProvider timeProvider,
    [FromKeyedServices(OutboxOptions.JsonOptionsKey)] JsonSerializerOptions jsonOptions) : IOutboxWriter
{
    public void Write<TEvent>(TEvent @event) where TEvent : class
    {
        var outboxMessage = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            OccurredOn = timeProvider.GetUtcNow(),
            Type = @event.GetType().AssemblyQualifiedName!,
            Content = JsonSerializer.Serialize(@event, jsonOptions)
        };

        dbContext.Add(outboxMessage);
    }
}
