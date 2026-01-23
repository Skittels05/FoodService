using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class CreateCustomerCommandHandler(
    IGenericRepository<Customer> customerRepository,
    IMapper mapper)
    : IRequestHandler<CreateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = mapper.Map<Customer>(request);
        var createdCustomer = await customerRepository.AddAsync(customer);
        return createdCustomer.Id;
    }
}
