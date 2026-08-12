namespace DeliveryService.BLL.Constants;

public class Constraints
{
    public const int OrderDeliveryAddressMaxLength = 250;
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
}
