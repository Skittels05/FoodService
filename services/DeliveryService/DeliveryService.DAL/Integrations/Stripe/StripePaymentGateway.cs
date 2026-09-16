using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Services.Interfaces;
using DeliveryService.DAL.Exceptions;
using DeliveryService.DAL.Integrations.Stripe.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DeliveryService.DAL.Integrations.Stripe;

public sealed class StripePaymentGateway(
    HttpClient httpClient,
    IOptions<StripeOptions> options,
    [FromKeyedServices(StripeOptions.JsonOptionsKey)] JsonSerializerOptions jsonOptions,
    ILogger<StripePaymentGateway> logger) : IPaymentGateway
{
    public const string ProviderName = "stripe";

    private const string PaymentIntentsPath = "/v1/payment_intents";
    private const string IdempotencyKeyHeader = "Idempotency-Key";
    private const int MinorUnitsPerUnit = 100;

    private static readonly HashSet<string> ZeroDecimalCurrencies = new(StringComparer.OrdinalIgnoreCase)
    {
        "bif", "clp", "djf", "gnf", "jpy", "kmf", "krw", "mga",
        "pyg", "rwf", "ugx", "vnd", "vuv", "xaf", "xof", "xpf"
    };

    private readonly StripeOptions _options = options.Value;

    public async Task<PaymentGatewayResult> CreatePaymentAsync(
        CreatePaymentRequest request,
        CancellationToken cancellationToken = default)
    {
        using var httpRequest = BuildRequest(request);

        using var response = await httpClient.SendAsync(httpRequest, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await ReadSuccessAsync(response, request, cancellationToken);
        }

        if ((int)response.StatusCode is >= 400 and < 500)
        {
            return await ReadDeclineAsync(response, request, cancellationToken);
        }

        throw new PaymentGatewayUnavailableException(ProviderName, response.StatusCode);
    }

    private HttpRequestMessage BuildRequest(CreatePaymentRequest request)
    {
        var parameters = new Dictionary<string, string>
        {
            ["amount"] = ToMinorUnits(request.Amount).ToString(CultureInfo.InvariantCulture),
            ["currency"] = _options.Currency,
            ["payment_method_types[]"] = "card",
            ["payment_method"] = _options.PaymentMethodId,
            ["confirm"] = "true",
            ["metadata[order_id]"] = request.OrderId.ToString(),
            ["metadata[payment_id]"] = request.PaymentId.ToString()
        };

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, PaymentIntentsPath)
        {
            Content = new FormUrlEncodedContent(parameters)
        };

        httpRequest.Headers.Add(IdempotencyKeyHeader, request.PaymentId.ToString());

        return httpRequest;
    }

    private async Task<PaymentGatewayResult> ReadSuccessAsync(
        HttpResponseMessage response,
        CreatePaymentRequest request,
        CancellationToken cancellationToken)
    {
        var intent = await ReadAsync<StripePaymentIntentResponse>(response, cancellationToken);

        if (string.IsNullOrWhiteSpace(intent?.Id))
        {
            throw new PaymentGatewayException(ProviderName, "returned a payment intent without an id.");
        }

        if (!IsPaid(intent.Status))
        {
            logger.LogWarning(
                "Stripe payment intent {IntentId} for payment {PaymentId} is in non-final status '{IntentStatus}'.",
                intent.Id, request.PaymentId, intent.Status);

            return PaymentGatewayResult.Declined(
                ProviderName,
                $"Payment intent is in status '{intent.Status}' and was not completed.");
        }

        return PaymentGatewayResult.Success(ProviderName, intent.Id);
    }

    private async Task<PaymentGatewayResult> ReadDeclineAsync(
        HttpResponseMessage response,
        CreatePaymentRequest request,
        CancellationToken cancellationToken)
    {
        var error = (await ReadAsync<StripeErrorResponse>(response, cancellationToken))?.Error;

        logger.LogWarning(
            "Stripe declined payment {PaymentId} for order {OrderId} with status {StatusCode}, code '{ErrorCode}'.",
            request.PaymentId, request.OrderId, (int)response.StatusCode, error?.DeclineCode ?? error?.Code ?? "unknown");

        var message = error?.Message ?? $"Stripe rejected the request with status code {(int)response.StatusCode}.";

        return PaymentGatewayResult.Declined(ProviderName, message);
    }

    private async Task<TResponse?> ReadAsync<TResponse>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        try
        {
            return await response.Content.ReadFromJsonAsync<TResponse>(jsonOptions, cancellationToken);
        }
        catch (Exception ex) when (ex is JsonException or NotSupportedException)
        {
            throw new PaymentGatewayResponseException(ProviderName, ex);
        }
    }

    private static bool IsPaid(string? status) => status is "succeeded";

    private long ToMinorUnits(decimal amount)
    {
        var factor = ZeroDecimalCurrencies.Contains(_options.Currency) ? 1 : MinorUnitsPerUnit;

        return (long)decimal.Round(amount * factor, 0, MidpointRounding.AwayFromZero);
    }
}
