using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Queries;

public record GetManagersByRestaurantQuery(Guid RestaurantId) : PageRequest, IRequest<PagedList<RestaurantManagerDto>>;
