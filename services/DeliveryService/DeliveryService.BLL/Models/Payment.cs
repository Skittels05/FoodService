using DeliveryService.BLL.Enums;

namespace DeliveryService.BLL.Models;

public class Payment : BaseModel
{
    public required Guid OrderId { get; set; }
    public required decimal Amount { get; set; }
    public required PaymentStatus Status { get; set; }
    public required PaymentMethod Method { get; set; }
    public string? ExternalTransactionId { get; set; } 
    public string? PaymentProvider { get; set; }
    public string? ErrorMessage { get; set; }
}
