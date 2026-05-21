using DeliveryService.BLL.DTOs;
using DeliveryService.BLL.Enums;
using RestaurantService.BLL.Common;

namespace DeliveryService.BLL.Services.Interfaces;

public interface IOrderService
{
    Task<PagedList<Order>?> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<PagedList<Order>?> GetByCustomerAsync (Guid userId, CancellationToken cancellationToken = default);
    Task <PagedList<Order>?> GetByCourierAsync (Guid courierId, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(CreateOrderDto request, CancellationToken cancellationToken = default);
    Task CancelAsync(CancelOrderDto request, CancellationToken cancellationToken = default);
    Task AssignCourierAsync(Guid orderId, Guid courierId, CancellationToken cancellationToken = default);
    Task UpdateStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken = default);
}
