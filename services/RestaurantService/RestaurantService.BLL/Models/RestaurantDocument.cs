using RestaurantService.BLL.Enums;

namespace RestaurantService.BLL.Models;

public class RestaurantDocument : BaseModel
{
    public Guid RestaurantId { get; set; }
    public DocumentType Type { get; set; }
    public string FileUrl { get; set; }
    public VerificationStatus Status { get; set; }
    public string? RejectionReason { get; set; }
}
