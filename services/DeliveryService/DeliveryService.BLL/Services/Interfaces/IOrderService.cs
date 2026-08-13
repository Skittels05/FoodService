using DeliveryService.BLL.Common;
using DeliveryService.BLL.DTOs;

namespace DeliveryService.BLL.Services.Interfaces;

public interface IOrderService
{
    Task<PagedList<OrderDto>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<OrderDto?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task<PagedList<OrderDto>> GetByCustomerIdAsync(Guid userId, PageRequest request, CancellationToken cancellationToken = default);
    Task<PagedList<OrderDto>> GetByCourierIdAsync(Guid courierId, PageRequest request, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(CreateOrderDto request, CancellationToken cancellationToken = default);
    Task ConfirmAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task StartPreparingAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task MarkReadyForPickupAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task AssignCourierAsync(Guid orderId, Guid courierId, CancellationToken cancellationToken = default);
    Task StartDeliveringAsync(Guid orderId, Guid courierId, CancellationToken cancellationToken = default);
    Task CompleteAsync(Guid orderId, CancellationToken cancellationToken = default);
    Task CancelAsync(CancelOrderDto request, CancellationToken cancellationToken = default);
}
