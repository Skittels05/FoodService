using AuthService.Application.DTO.Courier;
using AuthService.Domain.Common;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Queries;

public record GetAllCouriersQuery(int Page = 1, int PageSize = 10)
    : IRequest<PagedList<CourierDto>>;
