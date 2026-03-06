namespace RestaurantService.BLL.Models;

public class MenuItem : BaseModel
{
    public Guid RestaurantId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}
