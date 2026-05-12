using DeliveryService.BLL.Models;

public class OrderItem : BaseModel
{
    public required Guid OrderId { get; set; }
    public required Guid MenuItemId { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
}
