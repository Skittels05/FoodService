namespace RestaurantService.BLL.Models;

public class StopListItem : BaseModel
{
    public Guid LocationId { get; set; }
    public Guid MenuItemId { get; set; }
    public string Reason { get; set; }
}
