using AuthService.Domain.Enums;

namespace AuthService.Application.DTO.Courier;

public record CreateCourierDto(
    string Name,
    VehicleType VehicleType,
    string DocumentsPath,
    string PhotoVerificationPath
);
