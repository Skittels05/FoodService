namespace RestaurantService.BLL.Models;

public class Location : BaseModel
{
    public Guid RestaurantId { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public bool IsAcceptingOrders { get; set; }
    public List<StopListItem> StopList { get; set; }
}
