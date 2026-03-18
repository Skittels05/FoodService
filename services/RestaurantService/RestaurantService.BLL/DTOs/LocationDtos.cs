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

public record CreateLocationDto(string Address, double Latitude, double Longitude, bool IsAcceptingOrders);
public record UpdateLocationDto(string Address, double Latitude, double Longitude, bool IsAcceptingOrders);
