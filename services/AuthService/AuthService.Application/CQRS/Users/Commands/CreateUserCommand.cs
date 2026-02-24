using AuthService.Application.Common.Interfaces;
using AuthService.Domain.Enums;
using MediatR;

namespace AuthService.Application.CQRS.Users.Commands;

public record CreateUserCommand(
    string Auth0Id,
    string Email,
    string UserName,
    UserRole Role
) : IRequest<Guid>, ITransactionalCommand;
