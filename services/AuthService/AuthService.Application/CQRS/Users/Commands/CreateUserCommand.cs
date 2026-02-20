using AuthService.Application.Common.Interfaces;
using AuthService.Domain.Enums;
using MediatR;
using Destructurama.Attributed;

namespace AuthService.Application.CQRS.Users.Commands
{
    public record CreateUserCommand(
    string Email,
    string UserName,
    [property: LogMasked] string Password,
    UserRole Role
) : IRequest<Guid>, ITransactionalCommand;
}
