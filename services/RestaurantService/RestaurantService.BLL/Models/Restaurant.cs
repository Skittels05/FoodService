namespace RestaurantService.BLL.Models;

public class Restaurant : BaseModel
{
    public string Name { get; set; }
    public bool IsVerified { get; set; }
    public bool IsActive { get; set; }
    public List<RestaurantDocument> Documents { get; set; }
}
