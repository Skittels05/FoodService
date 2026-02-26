using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Users.Commands;

public record SyncAuth0UserCommand() : IRequest<Guid>, ITransactionalCommand;
