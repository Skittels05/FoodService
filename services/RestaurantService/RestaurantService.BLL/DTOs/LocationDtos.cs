namespace RestaurantService.BLL.DTOs;

public record LocationDto(
    Guid Id,
    Guid RestaurantId,
    string Address,
    double Latitude,
    double Longitude,
    bool IsAcceptingOrders,
    List<StopListItemDto> StopList
);

public record RestaurantNearbyDto(
    Guid LocationId,
    Guid RestaurantId,
    string RestaurantName,
    string Address,
    double DistanceInKm,
    double Latitude,
    double Longitude
);

public record GeoSearchResultDto(
    Guid LocationId,
    double Distance
);

public record CreateLocationDto(string Address, double Latitude, double Longitude, bool IsAcceptingOrders);
public record UpdateLocationDto(string Address, double Latitude, double Longitude, bool IsAcceptingOrders);
