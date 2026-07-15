using DeliveryService.BLL.Enums;

namespace DeliveryService.BLL.Models;

public class OrderStatusHistory: BaseModel
{
    public Guid OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public Guid? ChangedBy { get; set; }
}
