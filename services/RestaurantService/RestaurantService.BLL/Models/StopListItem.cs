using System.ComponentModel.DataAnnotations;
using RestaurantService.BLL.Constants;

namespace RestaurantService.BLL.Models;

public class StopListItem : BaseModel
{
    [Required]
    public Guid LocationId { get; set; }
    [Required]
    public Guid MenuItemId { get; set; }
    [MaxLength(ValidationConstants.StopListReasonMaxLength)]
    public string? Reason { get; set; } = string.Empty;
}
