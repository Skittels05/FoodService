using AuthService.Application.DTO.RestaurantManager;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManager.Queries;

public record GetManagerByUserIdQuery(Guid UserId) : IRequest<RestaurantManagerDto?>;
