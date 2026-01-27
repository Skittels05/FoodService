using AuthService.Application.DTO.Courier;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Queries;

public record GetAllCouriersQuery(int Page = PaginationConstants.DefaultPageNumber, int PageSize = PaginationConstants.DefaultPageSize)
    : IRequest<PagedList<CourierDto>>;
