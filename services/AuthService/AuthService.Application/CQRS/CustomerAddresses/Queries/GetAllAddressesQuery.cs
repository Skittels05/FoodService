using AuthService.Application.DTO.Customers;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

public record GetAllAddressesQuery(
    int Page = PaginationConstants.DefaultPageNumber,
    int PageSize = PaginationConstants.DefaultPageSize,
    string? SortBy = null,
    string? SortOrder = null
) : IRequest<PagedList<CustomerAddressDto>>;
