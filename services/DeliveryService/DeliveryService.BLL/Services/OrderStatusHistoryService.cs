using DeliveryService.BLL.Models;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.BLL.Services.Interfaces;

namespace DeliveryService.BLL.Services;

public class OrderStatusHistoryService(IOrderStatusHistoryRepository historyRepository) 
    : IOrderStatusHistoryService
{
    public async Task<List<OrderStatusHistory>> GetOrderHistoryAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await historyRepository.GetByOrderIdAsync(orderId, cancellationToken);
    }
}
