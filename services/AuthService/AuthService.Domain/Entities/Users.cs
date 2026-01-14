using AuthService.Domain.Enums;
using AuthService.Domain.ValueObjects;

namespace AuthService.Domain.Entities;


public class User
{
    public Guid Id { get; private set; }
    public Email Email { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected User() { }

    public User(Email email, UserRole role)
    {
        Id = Guid.NewGuid();
        Email = email;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}

