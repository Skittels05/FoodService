using System.ComponentModel.DataAnnotations;

namespace DeliveryService.DAL.Outbox;

public class OutboxOptions
{
    public const string SectionName = "Outbox";
    public const string JsonOptionsKey = "OutboxJsonOptions";
    [Required]
    [Range(1, 1000, ErrorMessage = "BatchSize must be between 1 and 1000.")]
    public int BatchSize { get; set; }
    [Required]
    [Range(1, 20, ErrorMessage = "MaxRetryCount must be between 1 and 20.")]
    public int MaxRetryCount { get; set; }
    [Required]
    [Range(1, 60, ErrorMessage = "IntervalInSeconds must be between 1 and 60.")]
    public int IntervalInSeconds { get; set; }
}
