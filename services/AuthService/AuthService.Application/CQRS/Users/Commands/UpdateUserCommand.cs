using AuthService.Domain.Enums;
using MediatR;

namespace AuthService.Application.CQRS.Users.Commands
{
    public record UpdateUserCommand(
    Guid Id,
    string Email,
    string UserName,
    string? PhoneNumber,
    UserRole Role
) : IRequest;
}
