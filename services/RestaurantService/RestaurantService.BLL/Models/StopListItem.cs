using RestaurantService.BLL.Enums;

namespace RestaurantService.BLL.Models;

public class StopListItem : BaseModel
{
    public required Guid LocationId { get; set; }
    public required Guid MenuItemId { get; set; }
    public required StopListReason Reason { get; set; }
    public string? Description { get; set; }
}
