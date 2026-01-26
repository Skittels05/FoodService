using AuthService.Application.CQRS.Customers.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class GetCustomerByUserIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    : IRequestHandler<GetCustomerByUserIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundByUserException(nameof(Customer), request.UserId);
        return mapper.Map<CustomerDto>(customer);
    }
}
