namespace RestaurantService.BLL.Models;

public class StopListItem : BaseModel
{
    public required Guid LocationId { get; set; }
    public required Guid MenuItemId { get; set; }
    public string? Reason { get; set; } = string.Empty;
}
