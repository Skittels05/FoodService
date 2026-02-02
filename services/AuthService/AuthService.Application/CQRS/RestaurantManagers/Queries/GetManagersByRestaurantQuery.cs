using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Queries;

public record GetManagersByRestaurantQuery(
    Guid RestaurantId,
    int Page = PaginationConstants.DefaultPageNumber,
    int PageSize = PaginationConstants.DefaultPageSize,
    string? SortBy = null, 
    string? SortOrder = null) : 
    IRequest<PagedList<RestaurantManagerDto>>;
