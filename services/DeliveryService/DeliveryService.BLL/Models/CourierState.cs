using DeliveryService.BLL.Enums;

namespace DeliveryService.BLL.Models;

public class CourierState
{
    public required Guid CourierId { get; set; } 
    public required CourierStatus Status { get; set; }
    public DateTimeOffset? LastSeenAt { get; set; }
}
