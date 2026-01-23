using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class DeleteCustomerCommandHandler(IGenericRepository<Customer> customerRepository)
    : IRequestHandler<DeleteCustomerCommand>
{
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        await customerRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
