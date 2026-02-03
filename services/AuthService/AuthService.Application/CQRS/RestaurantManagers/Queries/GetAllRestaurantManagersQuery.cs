using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Queries;

public record GetAllRestaurantManagersQuery : PageRequest, IRequest<PagedList<RestaurantManagerDto>>;
