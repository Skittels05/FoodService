using AuthService.Application.CQRS.Customers.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class GetCustomerByUserIdQueryHandler(
    IGenericRepository<Customer> customerRepository,
    IMapper mapper)
    : IRequestHandler<GetCustomerByUserIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Customer, bool>> predicate = c => c.UserId == request.UserId;

        var customers = await customerRepository.FindAsync(predicate);
        var customer = customers.FirstOrDefault()
            ?? throw new NotFoundByUserException(nameof(Customer), request.UserId);
        return mapper.Map<CustomerDto>(customer);
    }
}
