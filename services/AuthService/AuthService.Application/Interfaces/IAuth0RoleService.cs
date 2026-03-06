using AuthService.Domain.Enums;

namespace AuthService.Application.Interfaces;

public interface IAuth0RoleService
{
    Task AssignRoleAsync(string auth0UserId, UserRole role, CancellationToken cancellationToken);
    Task SetAsVerifiedAsync(string auth0UserId, CancellationToken cancellationToken = default);
    Task SetRestaurantIdAsync(string auth0UserId, Guid restaurantId, CancellationToken cancellationToken = default);
}
