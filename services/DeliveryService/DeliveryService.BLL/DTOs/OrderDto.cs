using DeliveryService.BLL.Enums;

namespace DeliveryService.BLL.DTOs;

public record CreateOrderDto(
    Guid CustomerId, 
    Guid RestaurantId,
    Guid RestaurantLocationId,
    string DeliveryAddress,
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
    string DeliveryAddress,
    string? CustomerComment,
    OrderCancellationReason? CancellationReason,
    string? CancellationComment,
    DateTime CreatedAt,
    List<OrderItemDto> Items
);

