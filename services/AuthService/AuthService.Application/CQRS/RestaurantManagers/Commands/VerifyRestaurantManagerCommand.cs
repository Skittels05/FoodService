using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Commands;

public record VerifyRestaurantManagerCommand(Guid ManagerId) : IRequest, ITransactionalCommand;
