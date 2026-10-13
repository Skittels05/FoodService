namespace DeliveryService.BLL.Events;

public record CourierOfferedEvent(Guid OrderId, Guid CourierId, DateTimeOffset ExpiresAt);

public record CourierAcceptedOrderEvent(Guid OrderId, Guid CourierId);

public record CourierArrivedAtRestaurantEvent(Guid OrderId, Guid CourierId, Guid RestaurantLocationId);

public record CourierArrivedAtCustomerEvent(Guid OrderId, Guid CourierId);
