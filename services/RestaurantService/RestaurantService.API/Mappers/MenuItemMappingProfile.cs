using RestaurantService.API.RequestModels;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Mappers;

public static class MenuItemMappingProfile
{
    public static CreateMenuItemDto ToDto(this CreateMenuItemRequest request, Guid restaurantId)
    {
        return new CreateMenuItemDto(
            restaurantId, 
            request.Name, 
            request.Price, 
            request.IsActive
        );
    }

    public static UpdateMenuItemDto ToDto(this UpdateMenuItemRequest request, Guid id)
    {
        return new UpdateMenuItemDto(
            id, 
            request.Name, 
            request.Price, 
            request.IsActive
        );
    }
}
