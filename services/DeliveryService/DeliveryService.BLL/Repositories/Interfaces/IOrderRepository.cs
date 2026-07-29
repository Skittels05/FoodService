using DeliveryService.BLL.Common;

namespace DeliveryService.BLL.Repositories.Interfaces;

public interface IOrderRepository: IGenericRepository<Order>
{
    Task<Order?> GetByIdWithPaymentsAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<PagedList<Order>> GetByCustomerAsync(Guid userId, PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false);
    Task<PagedList<Order>> GetByCourierAsync(Guid courierId, PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false);
}
