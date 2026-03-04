using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Domain.Common;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Queries;

public record GetManagersByRestaurantQuery : PageRequest, IRequest<PagedList<RestaurantManagerDto>>
{
    public Guid RestaurantId { get; set; }
    public bool? IsVerified { get; set; }
}
