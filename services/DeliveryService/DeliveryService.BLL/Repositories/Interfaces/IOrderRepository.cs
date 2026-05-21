using DeliveryService.BLL.Common;

namespace DeliveryService.BLL.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdWithPaymentsAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<PagedList<Order>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<PagedList<Order>> GetByCustomerAsync(Guid userId, PageRequest request, CancellationToken cancellationToken = default);
    Task<PagedList<Order>> GetByCourierAsync(Guid courierId, PageRequest request, CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);
}
