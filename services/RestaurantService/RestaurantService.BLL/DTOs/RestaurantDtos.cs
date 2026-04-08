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
    Guid Id,
    string Name
);

public record UpdateRestaurantStatusDto(
    Guid Id,
    bool IsActive
);
