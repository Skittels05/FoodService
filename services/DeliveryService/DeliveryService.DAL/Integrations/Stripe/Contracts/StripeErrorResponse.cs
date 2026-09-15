namespace DeliveryService.DAL.Integrations.Stripe.Contracts;

public record StripeErrorResponse(StripeError? Error);

public record StripeError(string? Type, string? Code, string? DeclineCode, string? Message);
