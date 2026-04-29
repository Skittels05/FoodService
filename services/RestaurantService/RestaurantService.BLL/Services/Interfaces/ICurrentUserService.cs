namespace RestaurantService.BLL.Interfaces;

public interface ICurrentUserService
{
    string? Auth0Id { get; }
    string? Email { get; }
    string? Username { get; }
    bool IsVerified { get; }
    Guid? RestaurantId { get; }
    string? Role { get; }
}
