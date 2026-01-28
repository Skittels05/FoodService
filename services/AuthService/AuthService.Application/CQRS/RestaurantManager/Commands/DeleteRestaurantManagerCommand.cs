using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Commands;

public record DeleteRestaurantManagerCommand(Guid Id) : IRequest;
