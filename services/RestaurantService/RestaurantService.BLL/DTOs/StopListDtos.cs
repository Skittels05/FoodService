using RestaurantService.BLL.Enums;

namespace RestaurantService.BLL.DTOs;

public record StopListItemDto(Guid Id, Guid LocationId, Guid MenuItemId, string Reason, string? Description);

public record AddStopListItemDto(Guid MenuItemId, StopListReason Reason, string? Description);
