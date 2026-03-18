using RestaurantService.BLL.Enums;

namespace RestaurantService.BLL.Models;

public class RestaurantDocument : BaseModel
{
    public required Guid RestaurantId { get; set; }
    public required DocumentType Type { get; set; }
    public required string FileUrl { get; set; }
    public required VerificationStatus Status { get; set; }
    public string? RejectionReason { get; set; } = string.Empty;
}
