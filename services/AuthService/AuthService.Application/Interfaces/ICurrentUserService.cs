using AuthService.Domain.Enums;

namespace AuthService.Application.Interfaces;

public interface ICurrentUserService
{
    string? Auth0Id { get; }
    string? Email { get; }
    string? Username { get; }
    UserRole Role { get; }
    bool IsVerified { get; }
}
