using DispatchR.Abstractions.Notification;

namespace DeliveryService.BLL.Events;

public record PaymentAuthorizedEvent(Guid OrderId, Guid PaymentId, decimal Amount) : INotification;

public record PaymentFailedEvent(Guid OrderId, Guid PaymentId, string ErrorMessage) : INotification;

public record RefundCompletedEvent(Guid OrderId, Guid PaymentId, decimal RefundedAmount) : INotification;
