using AuthService.Domain.Enums;

namespace AuthService.Application.DTO.Users;

public record UserAccountDto(
    Guid Id,
    string Auth0Id,
    string Email,
    string UserName,
    UserRole Role
);
