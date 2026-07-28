using DispatchR.Abstractions.Notification;

namespace DeliveryService.BLL.Events;

public record CourierInvitedEvent(Guid OrderId, Guid CourierId, DateTime ExpiresAt) : INotification;

public record CourierAcceptedOrderEvent(Guid OrderId, Guid CourierId) : INotification;

public record CourierArrivedAtRestaurantEvent(Guid OrderId, Guid CourierId, Guid RestaurantLocationId) : INotification;

public record CourierArrivedAtCustomerEvent(Guid OrderId, Guid CourierId) : INotification;
