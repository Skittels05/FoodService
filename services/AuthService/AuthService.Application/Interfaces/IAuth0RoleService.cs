namespace AuthService.Application.Interfaces;

public interface IAuth0RoleService
{
    Task AssignCourierRoleAsync(string auth0UserId, CancellationToken cancellationToken);
    Task AssignCustomerRoleAsync(string auth0UserId, CancellationToken cancellationToken);
    Task AssignRestaurantManagerRoleAsync(string auth0UserId, CancellationToken cancellationToken);
}
