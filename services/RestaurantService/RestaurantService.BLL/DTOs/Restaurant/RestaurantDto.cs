using RestaurantService.BLL.DTOs.RestaurantDocument;

namespace RestaurantService.BLL.DTOs.Restaurant;

public record RestaurantDto(
    Guid Id,
    string Name,
    bool IsVerified,
    bool IsActive,
    List<RestaurantDocumentDto> Documents
);

