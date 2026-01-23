using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;
using AuthService.Application.Exceptions;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Guid>
{
    private readonly IGenericRepository<Customer> _customerRepository;

    public UpdateCustomerCommandHandler(IGenericRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Customer), request.Id);
        
        customer.ChangeName(request.Name);
        var updatedCustomer = await _customerRepository.UpdateAsync(customer);
        return updatedCustomer.Id;
    }
}
