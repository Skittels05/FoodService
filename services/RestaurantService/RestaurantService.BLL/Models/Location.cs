using System.ComponentModel.DataAnnotations;
using RestaurantService.BLL.Constants;

namespace RestaurantService.BLL.Models;

public class Location : BaseModel
{
    [Required]
    public Guid RestaurantId { get; set; }
    [Required]
    [MaxLength(ValidationConstants.LocationAddressMaxLength)]
    public string Address { get; set; }
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
    [Required]
    public bool IsAcceptingOrders { get; set; }
    public List<StopListItem> StopList { get; set; } = [];
}
