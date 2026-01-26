using MediatR;

namespace AuthService.Application.CQRS.Couriers.Commands;

public record DeleteCourierCommand(Guid Id) : IRequest;
