namespace DeliveryService.BLL.Constants;

public class Constraints
{
    public const int AddressMaxLength = 250;
    public const int OrderCustomerCommentMaxLength = 500;
    public const int OrderCancellationCommentMaxLength = 500;
    public const int OrderStatusMaxLength = 50;
    public const int OrderCancellationReasonMaxLength = 50;

    public const int OrderItemNameMaxLength = 50;
    public const int OrderItemQuantityMin = 1;
    public const int OrderItemQuantityMax = 100;

    public const int PaymentStatusMaxLength = 50;
    public const int PaymentMethodMaxLength = 50;
    public const int PaymentExternalTransactionIdMaxLength = 256;
    public const int PaymentProviderMaxLength = 100;
    public const int PaymentErrorMessageMaxLength = 1000;
    
    public const int CourierStatusMaxLength = 50;
    public const double LatitudeMin = -90.0;
    public const double LatitudeMax = 90.0;
    public const double LongitudeMin = -180.0;
    public const double LongitudeMax = 180.0;
    public const int CourierLocationMaxAgeInSeconds = 60;
    public const int CourierLocationFutureToleranceInSeconds = 30;
}
