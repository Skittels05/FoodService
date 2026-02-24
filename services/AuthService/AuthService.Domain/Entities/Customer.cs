using AuthService.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Entities;

public class Customer : EntityBase
{
    public Guid UserId { get; private set; }
    [MaxLength(ValidationConstants.NameMaxLength)]
    public string Name { get; private set; }

    protected Customer() { }

    public Customer(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
    public void ChangeName(string name)
    {
        Name = name;
    }
}
