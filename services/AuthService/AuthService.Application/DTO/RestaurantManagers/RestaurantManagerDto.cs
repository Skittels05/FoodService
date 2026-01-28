namespace AuthService.Application.DTO.RestaurantManagers;

public record RestaurantManagerDto(
    Guid Id,
    Guid UserId,
    Guid ManagedRestaurantId,
    string Name
);
