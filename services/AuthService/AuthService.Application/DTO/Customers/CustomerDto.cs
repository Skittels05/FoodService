namespace AuthService.Application.DTO.Customers;

public record CustomerDto(
Guid Id,
Guid UserId,
string Name
);
