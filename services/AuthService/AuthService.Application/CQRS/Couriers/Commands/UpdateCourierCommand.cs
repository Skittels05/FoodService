using AuthService.Domain.Enums;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Commands
{
    public record UpdateCourierCommand(
    Guid Id,
    string Name,
    VehicleType VehicleType
) : IRequest;
}
