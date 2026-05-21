using DeliveryService.BLL.Enums;

namespace DeliveryService.BLL.DTOs;

public record CreateOrderDto(
    Guid CustomerId, 
    Guid RestaurantId,
    Guid RestaurantLocationId,
    string DeliveryAddress,
    string? CustomerComment
);

public record AddOrderItemDto(
    Guid OrderId,
    Guid MenuItemId,
    string Name,
    decimal Price,
    int Quantity
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
    string DeliveryAddress,
    string? CustomerComment,
    OrderCancellationReason? CancellationReason,
    string? CancellationComment,
    DateTime CreatedAt,
    List<OrderItemDto> Items
);

public record OrderItemDto(
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
