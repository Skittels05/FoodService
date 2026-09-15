namespace DeliveryService.BLL.Models;

public record CreatePaymentRequest(Guid PaymentId, Guid OrderId, decimal Amount);

public record PaymentGatewayResult(
    bool IsSuccess,
    string Provider,
    string? ExternalTransactionId,
    string? ErrorMessage)
{
    public static PaymentGatewayResult Success(string provider, string externalTransactionId) =>
        new(IsSuccess: true, provider, externalTransactionId, ErrorMessage: null);

    public static PaymentGatewayResult Declined(string provider, string errorMessage) =>
        new(IsSuccess: false, provider, ExternalTransactionId: null, errorMessage);
}
