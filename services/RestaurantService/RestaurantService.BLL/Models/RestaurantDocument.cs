using System.ComponentModel.DataAnnotations;
using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Constants;

namespace RestaurantService.BLL.Models;

public class RestaurantDocument : BaseModel
{
    [Required]
    public Guid RestaurantId { get; set; }

    [Required]
    public DocumentType Type { get; set; }

    [Required]
    [MaxLength(ValidationConstants.DocumentFileUrlMaxLength)]
    public string FileUrl { get; set; }

    [Required]
    public VerificationStatus Status { get; set; }

    [MaxLength(ValidationConstants.DocumentRejectionReasonMaxLength)]
    public string? RejectionReason { get; set; } = string.Empty;
}
