namespace RestaurantService.BLL.DTOs;

public record MenuItemDto(
    Guid Id, 
    Guid RestaurantId, 
    string Name, 
    decimal Price, 
    bool IsActive
);

public record CreateMenuItemDto(
    Guid RestaurantId,
    string Name, 
    decimal Price, 
    bool IsActive
);

public record UpdateMenuItemDto(
    Guid Id,
    string Name, 
    decimal Price, 
    bool IsActive
);
