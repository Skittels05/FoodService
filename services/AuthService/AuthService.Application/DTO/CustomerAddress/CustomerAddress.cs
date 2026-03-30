namespace AuthService.Application.DTO.Customers;

public record CustomerAddressDto(
    Guid Id,
    Guid CustomerId,
    string Address,
    double Latitude, 
    double Longitude,
    DateTime? LastUsedAt
);
