using System.Text.Json.Serialization;
using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Commands;

public record CreateCustomerCommand(
    string Name
) : IRequest<Guid>, ITransactionalCommand
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}
