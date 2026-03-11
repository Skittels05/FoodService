namespace RestaurantService.BLL.DTOs;

public record RestaurantDto(
    Guid Id,
    string Name,
    bool IsVerified,
    bool IsActive,
    List<RestaurantDocumentDto> Documents
);

public record CreateRestaurantDto(
    string Name
);

public record UpdateRestaurantDto(
    string Name
);
