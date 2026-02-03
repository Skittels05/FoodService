namespace AuthService.Application.DTO.Users;

public record UpdateUserDto(
    string Email,
    string UserName,
    string? PhoneNumber
);
