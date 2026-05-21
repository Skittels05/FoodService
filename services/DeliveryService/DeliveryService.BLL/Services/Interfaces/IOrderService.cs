using DeliveryService.BLL.DTOs;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Common;

namespace DeliveryService.BLL.Services.Interfaces;

public interface IOrderService
{
    Task<PagedList<OrderDto>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<OrderDto?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<PagedList<OrderDto>> GetByCustomerAsync (Guid userId, PageRequest request,CancellationToken cancellationToken = default);
    Task <PagedList<OrderDto>> GetByCourierAsync (Guid courierId, PageRequest request, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(CreateOrderDto request, CancellationToken cancellationToken = default);
    Task CancelAsync(CancelOrderDto request, CancellationToken cancellationToken = default);
    Task AssignCourierAsync(Guid orderId, Guid courierId, CancellationToken cancellationToken = default);
    Task UpdateStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken = default);
}
