using AuthService.Application.DTO.RestaurantManagers;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManager.Queries;

public record GetManagerByUserIdQuery(Guid UserId) : IRequest<RestaurantManagerDto?>;
