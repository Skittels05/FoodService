namespace AuthService.Application.DTO.Customers;

public record CustomerAddressDto(
    Guid Id,
    Guid CustomerId,
    string Address,
    DateTime? LastUsedAt
);
