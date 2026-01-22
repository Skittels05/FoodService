using AuthService.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Commands
{
    public record UpdateUserCommand(
    Guid Id,
    string Email,
    string UserName,
    string? PhoneNumber
) : IRequest<IdentityResult>;
}
