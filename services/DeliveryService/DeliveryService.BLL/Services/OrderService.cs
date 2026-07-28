using DeliveryService.BLL.Common;
using DeliveryService.BLL.DTOs;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Events;
using DeliveryService.BLL.Exceptions;
using DeliveryService.BLL.Mappers.Interfaces;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.BLL.Services.Interfaces;
using DispatchR;
using DispatchR.Abstractions.Notification;

namespace DeliveryService.BLL.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IMappingService mappingService,
    IMediator mediator) : IOrderService
{
    public async Task<Guid> CreateAsync(CreateOrderDto request, CancellationToken cancellationToken = default)
    {
        var order = mappingService.Map<CreateOrderDto, Order>(request);
        order.TotalAmount = order.Items.Sum(item => item.Price * item.Quantity);
        await orderRepository.AddAsync(order, cancellationToken);

        return order.Id;
    }

    public async Task ConfirmAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(orderId, cancellationToken);
        EnsureStatus(order, OrderStatus.Created);
        order.Status = OrderStatus.Confirmed;
        
        await UpdateAndPublishAsync(order, new OrderConfirmedEvent(order.Id), cancellationToken);
    }

    public async Task StartPreparingAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(orderId, cancellationToken);
        EnsureStatus(order, OrderStatus.Confirmed);
        order.Status = OrderStatus.Preparing;
        
        await UpdateAndPublishAsync(order, new OrderPreparingEvent(order.Id, order.RestaurantLocationId), cancellationToken);
    }

    public async Task MarkReadyForPickupAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(orderId, cancellationToken);
        EnsureStatus(order, OrderStatus.Preparing);

        order.Status = OrderStatus.ReadyForPickup;

        await UpdateAndPublishAsync(order, new OrderReadyForPickupEvent(order.Id, order.CourierId), cancellationToken);
    }

    public async Task AssignCourierAsync(Guid orderId, Guid courierId, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(orderId, cancellationToken);

        if (order.Status is OrderStatus.Delivered or OrderStatus.Cancelled)
        {
            throw new OrderFinalizedException(order.Id, order.Status);
        }

        if (order.CourierId.HasValue && order.CourierId != courierId)
        {
            throw new OrderCourierMismatchException(order.Id);
        }

        order.CourierId = courierId;
        await orderRepository.UpdateAsync(order, cancellationToken);
    }

    public async Task StartDeliveringAsync(Guid orderId, Guid courierId, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(orderId, cancellationToken);
        EnsureStatus(order, OrderStatus.ReadyForPickup);
        EnsureCourierMatches(order, courierId);
        order.Status = OrderStatus.Delivering;
        
        await UpdateAndPublishAsync(order, new OrderDeliveringEvent(order.Id, courierId), cancellationToken);
    }

    public async Task CompleteAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(orderId, cancellationToken);
        EnsureStatus(order, OrderStatus.Delivering);
        order.Status = OrderStatus.Delivered;
        order.IsPaid = true;

        await UpdateAndPublishAsync(order, new OrderDeliveredEvent(order.Id), cancellationToken);
    }

    public async Task CancelAsync(CancelOrderDto request, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(request.OrderId, cancellationToken);
    
        if (order.Status is OrderStatus.Delivered or OrderStatus.Cancelled)
        {
            throw new OrderCannotBeCancelledException(order.Id, order.Status);
        }

        order.Status = OrderStatus.Cancelled;
        order.CancellationReason = request.Reason;

        if (request.Reason == OrderCancellationReason.Other)
        {
            order.CancellationComment = request.Comment;
        }
        await UpdateAndPublishAsync(order, new OrderCancelledEvent(order.Id, request.Reason, request.Comment), cancellationToken);
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

    private async Task<Order> GetOrderOrThrowAsync(Guid orderId, CancellationToken cancellationToken)
    {
        return await orderRepository.GetByIdAsync(orderId, cancellationToken, trackChanges: true)
            ?? throw new NotFoundException(nameof(Order), orderId);
    }

    private static void EnsureStatus(Order order, OrderStatus expectedStatus)
    {
        if (order.Status != expectedStatus)
        {
            throw new InvalidOrderStateException(order.Id, order.Status, expectedStatus);
        }
    }

    private static void EnsureCourierMatches(Order order, Guid courierId)
    {
        if (order.CourierId != courierId)
        {
            throw new OrderCourierMismatchException(order.Id);
        }
    }

    private async Task UpdateAndPublishAsync(Order order, INotification? domainEvent, CancellationToken cancellationToken)
    {
        await orderRepository.UpdateAsync(order, cancellationToken);

        if (domainEvent is not null)
        {
            await mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
