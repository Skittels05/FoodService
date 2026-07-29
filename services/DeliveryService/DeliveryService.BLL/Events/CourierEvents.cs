namespace DeliveryService.BLL.Events;

public record CourierInvitedEvent(Guid OrderId, Guid CourierId, DateTime ExpiresAt);

public record CourierAcceptedOrderEvent(Guid OrderId, Guid CourierId);

public record CourierArrivedAtRestaurantEvent(Guid OrderId, Guid CourierId, Guid RestaurantLocationId);

public record CourierArrivedAtCustomerEvent(Guid OrderId, Guid CourierId);
