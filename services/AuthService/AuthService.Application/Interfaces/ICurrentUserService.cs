namespace AuthService.Application.Interfaces;

public interface ICurrentUserService
{
    string? Auth0Id { get; }
    string? Email { get; }
    string? Username { get; }
    string? Role { get; }
    bool IsVerified { get; }
}
