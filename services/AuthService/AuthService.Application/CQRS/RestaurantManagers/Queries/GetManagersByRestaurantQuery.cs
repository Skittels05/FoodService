using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Domain.Common;
using MediatR;
using System.Text.Json.Serialization;

namespace AuthService.Application.CQRS.RestaurantManagers.Queries;

public record GetManagersByRestaurantQuery : PageRequest, IRequest<PagedList<RestaurantManagerDto>>
{
    [JsonIgnore]
    public Guid RestaurantId { get; set; }
    public bool? IsVerified { get; set; }
}
