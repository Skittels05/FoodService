using DeliveryService.BLL.Enums;

namespace DeliveryService.BLL.Events;

public record OrderCreatedEvent(Guid OrderId, Guid CustomerId, Guid RestaurantId, Guid RestaurantLocationId, decimal TotalAmount);

public record OrderConfirmedEvent(Guid OrderId);

public record OrderPreparingEvent(Guid OrderId, Guid RestaurantLocationId);

public record OrderReadyForPickupEvent(Guid OrderId, Guid? CourierId);

public record OrderDeliveringEvent(Guid OrderId, Guid CourierId);

public record OrderDeliveredEvent(Guid OrderId);

public record OrderCancelledEvent(Guid OrderId, OrderCancellationReason Reason, string? Comment);
