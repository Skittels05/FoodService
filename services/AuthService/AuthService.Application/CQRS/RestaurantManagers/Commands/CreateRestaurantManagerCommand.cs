using System.Text.Json.Serialization;
using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Commands;

public record CreateRestaurantManagerCommand(
    Guid ManagedRestaurantId,
    string Name
) : IRequest<Guid>, ITransactionalCommand
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}
