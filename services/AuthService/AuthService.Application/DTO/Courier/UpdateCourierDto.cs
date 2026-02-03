using AuthService.Domain.Enums;

namespace AuthService.Application.DTO.Courier;

public record UpdateCourierDto(
    string Name,
    VehicleType VehicleType
);
