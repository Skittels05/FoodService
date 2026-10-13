namespace DeliveryService.BLL.Services.Interfaces;

public interface ICourierSearchScheduler
{
    Task ScheduleAsync(Guid orderId, TimeSpan? delay = null, CancellationToken cancellationToken = default);
}
