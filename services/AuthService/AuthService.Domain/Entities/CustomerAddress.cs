using AuthService.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Entities;

public class CustomerAddress : EntityBase
{
    public Guid CustomerId { get; private set; }
    [MaxLength(ValidationConstants.AddressMaxLength)]
    public string Address { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public DateTime? LastUsedAt { get; private set; }

    protected CustomerAddress() { }

    public CustomerAddress(Guid customerId, string address, double latitude, double longitude)
    {
        CustomerId = customerId;
        Address = address.Trim();
        Latitude = latitude;
        Longitude = longitude;
    }

    public void MarkAsUsed()
    {
        LastUsedAt = DateTime.UtcNow;
    }
}
