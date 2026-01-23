using AuthService.Domain.Enums;

namespace AuthService.Application.DTO.Courier
{
    public record CourierDto(
    Guid Id,
    Guid UserId,
    string Name,
    VehicleType VehicleType,
    bool IsVerified
);
}
