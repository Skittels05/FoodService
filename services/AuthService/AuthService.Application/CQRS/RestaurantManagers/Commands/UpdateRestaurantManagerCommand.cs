using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Commands;

public record UpdateRestaurantManagerCommand(
    Guid Id,
    Guid ManagedRestaurantId,
    string Name
) : IRequest, ITransactionalCommand;
