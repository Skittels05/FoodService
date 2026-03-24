namespace RestaurantService.BLL.Models;

public class Location : BaseModel
{
    public required Guid RestaurantId { get; set; }
    public required string Address { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public required bool IsAcceptingOrders { get; set; }
    public List<StopListItem> StopList { get; set; } = [];
    public Restaurant? Restaurant { get; set; }
}
