using AuthService.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain.Entities;

public class User : IdentityUser<Guid>, IEntityBase
{
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public UserRole Role { get; private set; }

    protected User() { }

    public User(string email, string userName, UserRole role)
    {
        Email = email;
        UserName = userName;
        Role = role;
    }
}
