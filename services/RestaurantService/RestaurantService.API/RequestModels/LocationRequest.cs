namespace RestaurantService.API.RequestModels;

public record CreateLocationRequest(
    string Address, 
    double Latitude, 
    double Longitude, 
    bool IsAcceptingOrders
);

public record UpdateLocationRequest(
    string Address, 
    double Latitude, 
    double Longitude, 
    bool IsAcceptingOrders
);

public record GetNearbyLocationsRequest(
    double Latitude,
    double Longitude,
    double RadiusKm = 5.0
);
