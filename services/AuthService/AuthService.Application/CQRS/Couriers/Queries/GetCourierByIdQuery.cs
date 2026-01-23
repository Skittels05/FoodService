using AuthService.Application.DTO.Courier;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Queries
{
    public record GetCourierByIdQuery(Guid Id) : IRequest<CourierDto?>;
}
