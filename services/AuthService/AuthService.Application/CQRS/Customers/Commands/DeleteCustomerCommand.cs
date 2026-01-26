using MediatR;

namespace AuthService.Application.CQRS.Customers.Commands;

public record DeleteCustomerCommand(Guid Id) : IRequest;
