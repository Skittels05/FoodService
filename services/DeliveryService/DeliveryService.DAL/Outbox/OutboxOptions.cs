namespace DeliveryService.DAL.Outbox;

public class OutboxOptions
{
    public const string SectionName = "Outbox";
    public const string JsonOptionsKey = "OutboxJsonOptions";
    public int BatchSize { get; set; } = 20;
    public int MaxRetryCount { get; set; } = 3;
    public int IntervalInSeconds { get; set; } = 5;
}
