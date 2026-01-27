using AuthService.Application.DTO.Customers;
using AuthService.Domain.Common;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Queries;

public record GetAllCustomersQuery(int Page = 1, int PageSize = 10)
    : IRequest<PagedList<CustomerDto>>;
