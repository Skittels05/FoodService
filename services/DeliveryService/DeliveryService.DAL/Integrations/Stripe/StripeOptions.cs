using System.ComponentModel.DataAnnotations;

namespace DeliveryService.DAL.Integrations.Stripe;

public class StripeOptions
{
    public const string SectionName = "Stripe";
    public const string JsonOptionsKey = "StripeJsonOptions";

    [Required]
    [Url(ErrorMessage = "BaseUrl must be a valid absolute URL.")]
    public string BaseUrl { get; set; } = null!;

    [Required(ErrorMessage = "SecretKey must be provided via user secrets or environment variables.")]
    public string SecretKey { get; set; } = null!;

    [Required]
    [RegularExpression("^[a-z]{3}$", ErrorMessage = "Currency must be a lowercase ISO 4217 code.")]
    public string Currency { get; set; } = null!;

    [Required(ErrorMessage = "PaymentMethodId must be provided.")]
    public string PaymentMethodId { get; set; } = null!;

    [Required]
    [Range(1, 60, ErrorMessage = "AttemptTimeoutInSeconds must be between 1 and 60.")]
    public int AttemptTimeoutInSeconds { get; set; }

    [Required]
    [Range(1, 300, ErrorMessage = "TotalTimeoutInSeconds must be between 1 and 300.")]
    public int TotalTimeoutInSeconds { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "RetryCount must be between 1 and 10.")]
    public int RetryCount { get; set; }

    [Required]
    [Range(1, 60, ErrorMessage = "RetryDelayInSeconds must be between 1 and 60.")]
    public int RetryDelayInSeconds { get; set; }

    [Required]
    [Range(2, 600, ErrorMessage = "CircuitBreakerSamplingDurationInSeconds must be between 2 and 600.")]
    public int CircuitBreakerSamplingDurationInSeconds { get; set; }
}
