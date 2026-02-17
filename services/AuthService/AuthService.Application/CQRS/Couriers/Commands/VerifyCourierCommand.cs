using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Commands;

public record VerifyCourierCommand(Guid Id) : IRequest, ITransactionalCommand;
