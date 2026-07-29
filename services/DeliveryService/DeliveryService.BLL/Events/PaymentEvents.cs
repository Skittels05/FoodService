namespace DeliveryService.BLL.Events;

public record PaymentAuthorizedEvent(Guid OrderId, Guid PaymentId, decimal Amount);

public record PaymentFailedEvent(Guid OrderId, Guid PaymentId, string ErrorMessage);

public record RefundCompletedEvent(Guid OrderId, Guid PaymentId, decimal RefundedAmount);
