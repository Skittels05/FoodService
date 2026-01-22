using AuthService.Domain.Enums;
using MediatR;

namespace AuthService.Application.CQRS.Users.Commands
{
    public record CreateUserCommand(
    string Email,
    string UserName,
    string Password,
    UserRole Role
) : IRequest<Guid>;
}
