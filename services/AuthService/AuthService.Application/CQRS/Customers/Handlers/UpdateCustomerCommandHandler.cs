using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class UpdateCustomerCommandHandler(IGenericRepository<Customer> customerRepository)
    : IRequestHandler<UpdateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Customer), request.Id);

        customer.ChangeName(request.Name);
        var updatedCustomer = await customerRepository.UpdateAsync(customer);
        return updatedCustomer.Id;
    }
}
