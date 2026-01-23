using MediatR;

namespace AuthService.Application.CQRS.Customers.Commands
{
    public record CreateCustomerCommand(
    Guid UserId,
    string Name
) : IRequest<Guid>;
}
