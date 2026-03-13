namespace RestaurantService.BLL.DTOs;

public record StopListItemDto(Guid Id, Guid LocationId, Guid MenuItemId, string Reason);
public record AddStopListItemDto(Guid MenuItemId, string Reason);
