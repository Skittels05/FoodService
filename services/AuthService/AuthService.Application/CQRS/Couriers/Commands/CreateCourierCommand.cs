using AuthService.Domain.Enums;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Commands;

public record CreateCourierCommand(
Guid UserId,
string Name,
VehicleType VehicleType,
string DocumentsPath,
string PhotoVerificationPath
) : IRequest<Guid>;
