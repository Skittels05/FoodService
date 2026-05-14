namespace DeliveryService.BLL.Enums;

public enum OrderCancellationReason
{
    None = 0,
    PaymentFailed = 1,
    CourierNotFound = 2,
    RestaurantRejected = 3,
    OutOfStock = 4,
    CustomerRefusedToPay = 5,
    DeliveryTooLong = 6,
    Other = 99
}
