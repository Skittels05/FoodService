using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Commands
{
    public record DeleteUserCommand(Guid Id) : IRequest<IdentityResult>;
}
