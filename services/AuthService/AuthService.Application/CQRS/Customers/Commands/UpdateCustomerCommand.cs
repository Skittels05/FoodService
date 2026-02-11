using AuthService.Application.Common.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Commands;

public record UpdateCustomerCommand(
Guid Id,
string Name
) : IRequest<Guid>, ITransactionalCommand;
