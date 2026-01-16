using AuthService.Domain.Enums;
using AuthService.Domain.ValueObjects;

namespace AuthService.Domain.Entities;

public class User: EntityBase
{
    public string Email { get; private set; }
    public UserRole Role { get; private set; }

    protected User() { }

    public User(string email, UserRole role)
    {
        Email = email;
        Role = role;
    }
}

