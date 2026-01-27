using AuthService.Application.CQRS.Customers.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class GetAllCustomersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllCustomersQuery, PagedList<CustomerDto>>
{
    public async Task<PagedList<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var pagedCustomers = await unitOfWork.CustomerRepository
            .GetAllAsync(request.Page, request.PageSize, cancellationToken);
        return mapper.Map<PagedList<CustomerDto>>(pagedCustomers);
    }
}