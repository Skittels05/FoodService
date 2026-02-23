using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Users.Commands;

public record UpdateUserCommand(
    Guid Id,
    string Email,
    string UserName
) : IRequest, ITransactionalCommand;
