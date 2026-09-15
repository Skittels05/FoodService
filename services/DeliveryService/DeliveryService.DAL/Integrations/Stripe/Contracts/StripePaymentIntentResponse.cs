namespace DeliveryService.DAL.Integrations.Stripe.Contracts;

public record StripePaymentIntentResponse(string Id, string Status);
