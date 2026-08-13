namespace DeliveryService.BLL.Services;

using DeliveryService.BLL.Common;
using DeliveryService.BLL.DTOs;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Events;
using DeliveryService.BLL.Exceptions;
using DeliveryService.BLL.Mappers.Interfaces;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.BLL.Services.Interfaces;
using Wolverine;

public class OrderService(
    IOrderRepository orderRepository,
    IMappingService mappingService,
    IUnitOfWork unitOfWork,
    IMessageBus bus) 
    : BaseService<Order, OrderDto>(orderRepository, mappingService, unitOfWork, bus), IOrderService
{
    public async Task<Guid> CreateAsync(CreateOrderDto request, CancellationToken cancellationToken = default)
    {
        var order = MappingService.Map<CreateOrderDto, Order>(request);
        order.TotalAmount = order.Items.Sum(item => item.Price * item.Quantity);
        
        Repository.Add(order);
        
        await SaveAndPublishAsync(new OrderCreatedEvent(
            order.Id, 
            order.CustomerId, 
            order.RestaurantId, 
            order.RestaurantLocationId, 
            order.TotalAmount), cancellationToken);

        return order.Id;
    }

    public async Task ConfirmAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await GetEntityOrThrowAsync(orderId, trackChanges: true, cancellationToken);
        EnsureStatus(order, OrderStatus.Created);
        order.Status = OrderStatus.Confirmed;
        
        await SaveAndPublishAsync(new OrderConfirmedEvent(order.Id), cancellationToken);
    }

    public async Task StartPreparingAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await GetEntityOrThrowAsync(orderId, trackChanges: true, cancellationToken);
        EnsureStatus(order, OrderStatus.Confirmed);
        order.Status = OrderStatus.Preparing;
        
        await SaveAndPublishAsync(new OrderPreparingEvent(order.Id, order.RestaurantLocationId), cancellationToken);
    }

    public async Task MarkReadyForPickupAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await GetEntityOrThrowAsync(orderId, trackChanges: true, cancellationToken);
        EnsureStatus(order, OrderStatus.Preparing);

        order.Status = OrderStatus.ReadyForPickup;

        await SaveAndPublishAsync(new OrderReadyForPickupEvent(order.Id, order.CourierId), cancellationToken);
    }

    public async Task AssignCourierAsync(Guid orderId, Guid courierId, CancellationToken cancellationToken = default)
    {
        var order = await GetEntityOrThrowAsync(orderId, trackChanges: true, cancellationToken);

        if (order.Status is OrderStatus.Delivered or OrderStatus.Cancelled)
        {
            throw new OrderFinalizedException(order.Id, order.Status);
        }

        if (order.CourierId.HasValue && order.CourierId != courierId)
        {
            throw new OrderCourierMismatchException(order.Id);
        }

        order.CourierId = courierId;
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task StartDeliveringAsync(Guid orderId, Guid courierId, CancellationToken cancellationToken = default)
    {
        var order = await GetEntityOrThrowAsync(orderId, trackChanges: true, cancellationToken);
        EnsureStatus(order, OrderStatus.ReadyForPickup);
        EnsureCourierMatches(order, courierId);
        order.Status = OrderStatus.Delivering;
        
        await SaveAndPublishAsync(new OrderDeliveringEvent(order.Id, courierId), cancellationToken);
    }

    public async Task CompleteAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var order = await GetEntityOrThrowAsync(orderId, trackChanges: true, cancellationToken);
        EnsureStatus(order, OrderStatus.Delivering);
        order.Status = OrderStatus.Delivered;
        order.IsPaid = true;

        await SaveAndPublishAsync(new OrderDeliveredEvent(order.Id), cancellationToken);
    }

    public async Task CancelAsync(CancelOrderDto request, CancellationToken cancellationToken = default)
    {
        var order = await GetEntityOrThrowAsync(request.OrderId, trackChanges: true, cancellationToken);
    
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

        await SaveAndPublishAsync(new OrderCancelledEvent(order.Id, request.Reason, request.Comment), cancellationToken);
    }

    public async Task<PagedList<OrderDto>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default)
    {
        var orders = await orderRepository.GetAllAsync(request, cancellationToken);
        return MappingService.MapPagedList<Order, OrderDto>(orders);
    }

    public async Task<PagedList<OrderDto>> GetByCustomerIdAsync(Guid userId, PageRequest request, CancellationToken cancellationToken = default)
    {
        var orders = await orderRepository.GetByCustomerIdAsync(userId, request, cancellationToken);
        return MappingService.MapPagedList<Order, OrderDto>(orders);
    }

    public async Task<PagedList<OrderDto>> GetByCourierIdAsync(Guid courierId, PageRequest request, CancellationToken cancellationToken = default)
    {
        var orders = await orderRepository.GetByCourierIdAsync(courierId, request, cancellationToken);
        return MappingService.MapPagedList<Order, OrderDto>(orders);
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
}
