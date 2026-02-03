using AuthService.Application.DTO.RestaurantManagers;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Queries;

public record GetManagerByUserIdQuery(Guid UserId) : IRequest<RestaurantManagerDto?>;
