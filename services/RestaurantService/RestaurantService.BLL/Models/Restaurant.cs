namespace RestaurantService.BLL.Models;

public class Restaurant : BaseModel
{
    public required string Name { get; set; }
    public required bool IsVerified { get; set; }
    public required bool IsActive { get; set; }
    public List<RestaurantDocument> Documents { get; set; } = [];
}
