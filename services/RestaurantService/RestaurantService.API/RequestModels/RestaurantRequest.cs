namespace RestaurantService.API.RequestModels;

public record CreateRestaurantRequest(
    string Name
);

public record UpdateRestaurantRequest(
    string Name
);

public record UpdateRestaurantStatusRequest(
    bool IsActive
);
