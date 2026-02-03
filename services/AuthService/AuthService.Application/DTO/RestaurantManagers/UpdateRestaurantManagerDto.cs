namespace AuthService.Application.DTO.RestaurantManagers;

public record UpdateRestaurantManagerDto(
    Guid ManagedRestaurantId,
    string Name
);
