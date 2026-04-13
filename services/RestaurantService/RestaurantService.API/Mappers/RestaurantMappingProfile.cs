using RestaurantService.API.RequestModels;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Mappers;

public static class RestaurantMappingProfile
{
    public static CreateRestaurantDto ToDto(this CreateRestaurantRequest request)
    {
        return new CreateRestaurantDto(request.Name);
    }
    
    public static UpdateRestaurantDto ToDto(this UpdateRestaurantRequest request, Guid id)
    {
        return new UpdateRestaurantDto(id, request.Name);
    }

    public static UpdateRestaurantStatusDto ToDto(this UpdateRestaurantStatusRequest request, Guid id)
    {
        return new UpdateRestaurantStatusDto(id, request.IsActive);
    }
}
