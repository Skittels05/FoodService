using AuthService.Application.DTO.Customers;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Queries;

public record GetCustomerAddressesQuery(
    Guid CustomerId,
    int Page = PaginationConstants.DefaultPageNumber,
    int PageSize = PaginationConstants.DefaultPageSize
) : IRequest<PagedList<CustomerAddressDto>>;
