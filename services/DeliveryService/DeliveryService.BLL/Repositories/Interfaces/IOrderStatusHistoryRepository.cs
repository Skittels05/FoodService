using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Repositories.Interfaces;

public interface IOrderStatusHistoryRepository: IGenericRepository<OrderStatusHistory>
{
    Task<List<OrderStatusHistory>> GetByOrderIdAsync(Guid orderId,  CancellationToken cancellationToken = default);
}
