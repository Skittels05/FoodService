using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly IGenericRepository<Customer> _customerRepository;

    public CreateCustomerCommandHandler(IGenericRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.UserId, request.Name);
        var createdCustomer = await _customerRepository.AddAsync(customer);
        return createdCustomer.Id;
    }
}
