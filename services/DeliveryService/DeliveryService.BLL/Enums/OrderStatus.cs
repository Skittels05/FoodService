namespace DeliveryService.BLL.Enums;

public enum OrderStatus
{
    None = 0,
    Created = 1,
    AwaitingPayment = 2,
    Paid = 3,
    Confirmed = 4,
    Preparing = 5,
    ReadyForPickup = 6,
    Delivering = 7,
    Delivered = 8,
    Cancelled = 9
}
