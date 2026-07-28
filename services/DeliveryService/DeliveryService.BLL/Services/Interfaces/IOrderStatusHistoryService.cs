using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Services.Interfaces;

public interface IOrderStatusHistoryService
{
    Task<List<OrderStatusHistory>> GetOrderHistoryAsync(Guid orderId, CancellationToken cancellationToken = default);
}
