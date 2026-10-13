using DeliveryService.BLL.Events;
using DeliveryService.BLL.Services.Interfaces;

namespace DeliveryService.BLL.Handlers;

public class CourierSearchHandler(ICourierSearchScheduler courierSearchScheduler)
{
    public Task Handle(OrderPreparingEvent @event, CancellationToken cancellationToken)
    {
        return courierSearchScheduler.ScheduleAsync(@event.OrderId, cancellationToken: cancellationToken);
    }

    public Task Handle(OrderReadyForPickupEvent @event, CancellationToken cancellationToken)
    {
        if (@event.CourierId is not null)
        {
            return Task.CompletedTask;
        }

        return courierSearchScheduler.ScheduleAsync(@event.OrderId, cancellationToken: cancellationToken);
    }
}
