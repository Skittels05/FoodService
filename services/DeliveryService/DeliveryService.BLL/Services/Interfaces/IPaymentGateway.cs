using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Services.Interfaces;

public interface IPaymentGateway
{
    Task<PaymentGatewayResult> CreatePaymentAsync(CreatePaymentRequest request, CancellationToken cancellationToken = default);
}
