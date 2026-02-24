using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Users.Commands;

public record DeleteUserCommand(Guid Id) : IRequest;
