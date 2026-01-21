using AuthService.Domain.Enums;

namespace AuthService.Application.DTO.Users
{
    public record UserAccountDto(
    Guid Id,
    string Email,
    string UserName,
    UserRole Role,
    string? PhoneNumber,
    bool IsPhoneNumberConfirmed,
    bool IsTwoFactorEnabled
);
}
