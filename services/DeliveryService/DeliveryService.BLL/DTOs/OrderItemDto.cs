namespace DeliveryService.BLL.DTOs;

public record OrderItemDto(
    Guid MenuItemId,
    string Name,
    decimal Price,
    int Quantity
);
