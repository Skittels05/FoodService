using AuthService.Application.CQRS.Customers.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class GetCustomerByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetCustomerByUserIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.CustomerRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundByUserException(nameof(Customer), request.UserId);
        return mapper.Map<CustomerDto>(customer);
    }
}
