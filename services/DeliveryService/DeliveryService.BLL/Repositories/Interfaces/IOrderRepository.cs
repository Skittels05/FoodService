using DeliveryService.BLL.Common;

namespace DeliveryService.BLL.Repositories.Interfaces;

public interface IOrderRepository: IGenericRepository<Order>
{
    Task<Order?> GetByIdWithPaymentsAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<PagedList<Order>> GetByCustomerIdAsync(Guid userId, PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false);
    Task<PagedList<Order>> GetByCourierIdAsync(Guid courierId, PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false);
}
