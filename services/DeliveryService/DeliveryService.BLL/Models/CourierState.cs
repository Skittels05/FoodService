namespace DeliveryService.BLL.Models;


public class CourierState
{
    public required Guid CourierId { get; set; } 
    public required bool IsOnline { get; set; }
    public required bool IsAvailable { get; set; }
}
