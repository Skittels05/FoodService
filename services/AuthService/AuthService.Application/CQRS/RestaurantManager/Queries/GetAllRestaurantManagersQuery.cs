using AuthService.Application.DTO.RestaurantManager;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Queries;

public record GetAllRestaurantManagersQuery(
    int Page = PaginationConstants.DefaultPageNumber,
    int PageSize = PaginationConstants.DefaultPageSize
) : IRequest<PagedList<RestaurantManagerDto>>;
