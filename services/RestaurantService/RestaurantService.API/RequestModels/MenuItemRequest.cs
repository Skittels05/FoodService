namespace RestaurantService.API.RequestModels;

public record CreateMenuItemRequest(
    string Name, 
    decimal Price, 
    bool IsActive
);

public record UpdateMenuItemRequest(
    string Name, 
    decimal Price, 
    bool IsActive
);
