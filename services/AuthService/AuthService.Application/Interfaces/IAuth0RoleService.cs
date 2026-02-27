using AuthService.Domain.Enums;

namespace AuthService.Application.Interfaces;

public interface IAuth0RoleService
{
    Task AssignRoleAsync(string auth0UserId, UserRole role, CancellationToken cancellationToken);
}
