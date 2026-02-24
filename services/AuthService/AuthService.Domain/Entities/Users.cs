using AuthService.Domain.Constants;
using AuthService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AuthService.Domain.Entities;

public class User : EntityBase
{

    public string Auth0Id { get; private set; }
    [MaxLength(ValidationConstants.EmailMaxLength)]
    public string Email { get; private set; }
    [MaxLength(ValidationConstants.UserNameMaxLength)]
    public string UserName { get; private set; }
    public UserRole Role { get; private set; }

    protected User() { }

    public User(string auth0Id, string email, string userName, UserRole role = UserRole.None)
    {
        Auth0Id = auth0Id;
        Email = email;
        UserName = userName;
        Role = role;
    }

    public void AssignRole(UserRole role)
    {
        Role = role;
    }
}
