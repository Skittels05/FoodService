using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Models;

public class Order : BaseModel
{
    public required Guid CustomerId { get; set; }
    public required Guid RestaurantId { get; set; }
    public Guid? CourierId { get; set; }
    public required OrderStatus Status { get; set; }
    public required decimal TotalAmount { get; set; }
    public required string DeliveryAddress { get; set; } = string.Empty;
    public string? CustomerComment { get; set; }

    public List<OrderItem> Items { get; set; } = [];
    public List<Payment> Payments { get; set; } = [];
}
