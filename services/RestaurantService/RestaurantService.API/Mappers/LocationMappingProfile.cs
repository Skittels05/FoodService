using RestaurantService.API.RequestModels;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Mappers;

public static class LocationMappingProfile
{
    public static CreateLocationDto ToDto(this CreateLocationRequest request, Guid restaurantId)
    {
        return new CreateLocationDto(
            restaurantId, 
            request.Address, 
            request.Latitude, 
            request.Longitude, 
            request.IsAcceptingOrders
        );
    }

    public static UpdateLocationDto ToDto(this UpdateLocationRequest request, Guid id)
    {
        return new UpdateLocationDto(
            id, 
            request.Address, 
            request.Latitude, 
            request.Longitude, 
            request.IsAcceptingOrders
        );
    }

    public static GetNearbyLocationsDto ToDto(this GetNearbyLocationsQuery query)
    {
        return new GetNearbyLocationsDto(
            query.Latitude, 
            query.Longitude, 
            query.RadiusKm
        );
    }
}
