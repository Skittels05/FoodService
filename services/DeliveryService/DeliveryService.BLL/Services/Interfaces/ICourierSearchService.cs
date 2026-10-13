namespace DeliveryService.BLL.Services.Interfaces;

public interface ICourierSearchService
{
    Task SearchAsync(Guid orderId, CancellationToken cancellationToken = default);
}
