namespace RestaurantService.BLL.Models;

public class MenuItem : BaseModel
{
    public required Guid RestaurantId { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required bool IsActive { get; set; }
}
