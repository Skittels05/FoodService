namespace DeliveryService.BLL.Enums;

public enum OrderStatus
{
    None = 0,
    Created = 1,
    Confirmed = 2,
    Preparing = 3,
    ReadyForPickup = 4,
    Delivering = 5,
    Delivered = 6,
    Cancelled = 7
}
