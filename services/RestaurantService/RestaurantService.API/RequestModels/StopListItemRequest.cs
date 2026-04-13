using RestaurantService.BLL.Enums;

namespace RestaurantService.API.RequestModels;

public record AddStopListItemRequest(
    Guid MenuItemId, 
    StopListReason Reason, 
    string? Description
);
