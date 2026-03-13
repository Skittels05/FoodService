using System.ComponentModel.DataAnnotations;
using RestaurantService.BLL.Constants;

namespace RestaurantService.BLL.Models;

public class MenuItem : BaseModel
{
    [Required]
    public Guid RestaurantId { get; set; }
    [Required]
    [MaxLength(ValidationConstants.MenuItemNameMaxLength)]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public bool IsActive { get; set; }
}
