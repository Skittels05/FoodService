using MediatR;

namespace AuthService.Application.CQRS.Couriers.Commands;

public record VerifyCourierCommand(Guid Id) : IRequest;
