namespace RestaurantService.BLL.DTOs;

public record MenuItemDto(Guid Id, Guid RestaurantId, string Name, decimal Price, bool IsActive);
public record CreateMenuItemDto(string Name, decimal Price, bool IsActive);
public record UpdateMenuItemDto(string Name, decimal Price, bool IsActive);
