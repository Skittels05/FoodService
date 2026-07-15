using DeliveryService.BLL.Common;
using DeliveryService.BLL.DTOs;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.BLL.Mappers.Interfaces;
using DeliveryService.BLL.Services.Interfaces;
using DeliveryService.BLL.Exceptions;

namespace DeliveryService.BLL.Services;

public class OrderService(
    IOrderRepository orderRepository,
    ICourierStateRepository courierStateRepository,
    IMappingService mappingService) : IOrderService
{
    public async Task<Guid> CreateAsync(CreateOrderDto request, CancellationToken cancellationToken = default)
    {
        var order = mappingService.Map<CreateOrderDto, Order>(request);
        order.TotalAmount = order.Items.Sum(item => item.Price * item.Quantity);

        await orderRepository.AddAsync(order, cancellationToken);

        return order.Id;
    }

    public async Task UpdateStatusAsync(Guid orderId, OrderStatus newStatus, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(orderId, cancellationToken, trackChanges:true)
            ?? throw new NotFoundException(nameof(Order), orderId);

        order.Status = newStatus;

        await orderRepository.UpdateAsync(order, cancellationToken);
    }

    public async Task AssignCourierAsync(Guid orderId, Guid courierId, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(orderId, cancellationToken, trackChanges:true)
            ?? throw new NotFoundException(nameof(Order), orderId);

        order.CourierId = courierId;
        
        await orderRepository.UpdateAsync(order, cancellationToken);
    }

    public async Task CancelAsync(CancelOrderDto request, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdWithPaymentsAsync(request.OrderId, cancellationToken)
            ?? throw new NotFoundException(nameof(Order), request.OrderId);

        order.Status = OrderStatus.Cancelled;
        order.CancellationReason = request.Reason;
        
        if (request.Reason == OrderCancellationReason.Other)
        {
            order.CancellationComment = request.Comment;
        }

        foreach (var payment in order.Payments.Where(p => p.Status == PaymentStatus.Pending))
        {
            payment.Status = PaymentStatus.Failed;
            payment.ErrorMessage = "Order was cancelled";
        }

        await orderRepository.UpdateAsync(order, cancellationToken);

        if (order.CourierId.HasValue)
        {
            var courierState = await courierStateRepository.GetByIdAsync(order.CourierId.Value, cancellationToken, trackChanges:true);
            if (courierState is not null)
            {
                courierState.IsAvailable = true;
                await courierStateRepository.UpdateAsync(courierState, cancellationToken);
            }
        }
    }

    public async Task<OrderDto?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await orderRepository.GetByIdAsync(orderId, cancellationToken);
        return order is null ? null : mappingService.Map<Order, OrderDto>(order);
    }

    public async Task<PagedList<OrderDto>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default)
    {
        var orders = await orderRepository.GetAllAsync(request, cancellationToken);
        return mappingService.MapPagedList<Order, OrderDto>(orders);
    }

    public async Task<PagedList<OrderDto>> GetByCustomerAsync(Guid userId, PageRequest request, CancellationToken cancellationToken = default)
    {
        var orders = await orderRepository.GetByCustomerAsync(userId, request, cancellationToken);
        return mappingService.MapPagedList<Order, OrderDto>(orders);
    }

    public async Task<PagedList<OrderDto>> GetByCourierAsync(Guid courierId, PageRequest request, CancellationToken cancellationToken = default)
    {
        var orders = await orderRepository.GetByCourierAsync(courierId, request, cancellationToken);
        return mappingService.MapPagedList<Order, OrderDto>(orders);
    }
}
