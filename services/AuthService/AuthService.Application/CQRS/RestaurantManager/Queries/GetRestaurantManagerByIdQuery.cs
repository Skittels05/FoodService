using AuthService.Application.DTO.RestaurantManager;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManager.Queries;

public record GetRestaurantManagerByIdQuery(Guid Id) : IRequest<RestaurantManagerDto?>;
