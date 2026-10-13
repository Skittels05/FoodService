using DeliveryService.BLL.Enums;

namespace DeliveryService.BLL.DTOs;

public record CreateOrderDto(
    Guid CustomerId, 
    Guid RestaurantId,
    Guid RestaurantLocationId,
    GeoAddressDto Delivery,
    string? CustomerComment,
    List<CreateOrderItemDto> Items
);

public record CreateOrderItemDto(
    Guid MenuItemId,
    string Name,
    decimal Price,
    int Quantity
);

public record CancelOrderDto(
    Guid OrderId,
    OrderCancellationReason Reason,
    string? Comment
);

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    Guid RestaurantId,
    Guid RestaurantLocationId,
    Guid? CourierId,
    OrderStatus Status,
    bool IsPaid,
    decimal TotalAmount,
    GeoAddressDto Delivery,
    GeoAddressDto Pickup,
    string? CustomerComment,
    OrderCancellationReason? CancellationReason,
    string? CancellationComment,
    DateTimeOffset CreatedAt,
    List<OrderItemDto> Items
);
