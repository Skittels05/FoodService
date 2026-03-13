using System.ComponentModel.DataAnnotations;
using RestaurantService.BLL.Constants;

namespace RestaurantService.BLL.Models;

public class Restaurant : BaseModel
{
    [Required]
    [MaxLength(ValidationConstants.RestaurantNameMaxLength)]
    public string Name { get; set; }
    [Required]
    public bool IsVerified { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public List<RestaurantDocument> Documents { get; set; } = [];
}
