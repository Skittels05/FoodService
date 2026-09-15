using System.Net;

namespace DeliveryService.DAL.Exceptions;

public class PaymentGatewayException(string provider, string message, Exception? innerException = null)
    : Exception($"Payment provider '{provider}' failed: {message}", innerException);

public class PaymentGatewayUnavailableException(string provider, HttpStatusCode statusCode)
    : PaymentGatewayException(provider, $"responded with status code {(int)statusCode} ({statusCode}).");

public class PaymentGatewayResponseException(string provider, Exception innerException)
    : PaymentGatewayException(provider, "returned a response that could not be read.", innerException);
