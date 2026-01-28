using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain.Entities;

public class User : IdentityUser<Guid>, IEntityBase
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public UserRole Role { get; private set; }

    protected User() { }

    public User(string email, string userName, UserRole role)
    {
        Email = email;
        UserName = userName;
        Role = role;
    }
}
