using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Commands;

public record VerifyManagerByRestaurantCommand(Guid RestaurantId) : IRequest;
