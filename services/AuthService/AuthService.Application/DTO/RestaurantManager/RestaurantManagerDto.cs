namespace AuthService.Application.DTO.RestaurantManager;

public record RestaurantManagerDto(
    Guid Id,
    Guid UserId,
    Guid ManagedRestaurantId,
    string Name
);
