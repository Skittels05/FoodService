using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Commands;

public record CreateRestaurantManagerCommand(
    Guid UserId,
    Guid ManagedRestaurantId,
    string Name
) : IRequest<Guid>;
