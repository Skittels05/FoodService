using RestaurantService.API.RequestModels;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Mappers;

public static class StopListMappingProfile
{
    public static AddStopListItemDto ToDto(this AddStopListItemRequest request, Guid locationId)
    {
        return new AddStopListItemDto(
            locationId, 
            request.MenuItemId, 
            request.Reason, 
            request.Description
        );
    }
}
