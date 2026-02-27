using System.Text.Json.Serialization;
using AuthService.Application.Common.Interfaces;
using AuthService.Domain.Enums;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Commands;

public record CreateCourierCommand(
    string Name,
    VehicleType VehicleType,
    string DocumentsPath,
    string PhotoVerificationPath
) : IRequest<Guid>, ITransactionalCommand
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}
