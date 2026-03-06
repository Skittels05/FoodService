using RestaurantService.BLL.Enums;
using System.ComponentModel.DataAnnotations;

namespace RestaurantService.BLL.Models;

public class RestaurantDocument : BaseModel
{
    [Required]
    public Guid RestaurantId { get; set; }
    [Required]
    public DocumentType Type { get; set; }
    [Required]
    public string FileUrl { get; set; }
    [Required]
    public VerificationStatus Status { get; set; }
    public string? RejectionReason { get; set; } = string.Empty;
}
