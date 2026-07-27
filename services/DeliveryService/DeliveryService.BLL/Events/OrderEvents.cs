using DeliveryService.BLL.Enums;
using DispatchR.Abstractions.Notification;

namespace DeliveryService.BLL.Events;

public record OrderConfirmedEvent(Guid OrderId) : INotification;
public record OrderPreparingEvent(Guid OrderId, Guid RestaurantLocationId) : INotification;
public record OrderReadyForPickupEvent(Guid OrderId, Guid? CourierId) : INotification;
public record OrderDeliveringEvent(Guid OrderId, Guid CourierId) : INotification;
public record OrderDeliveredEvent(Guid OrderId) : INotification;
public record OrderCancelledEvent(Guid OrderId, OrderCancellationReason Reason, string? Comment) : INotification;
