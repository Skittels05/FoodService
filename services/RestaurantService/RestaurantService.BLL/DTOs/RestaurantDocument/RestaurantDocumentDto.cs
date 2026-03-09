namespace RestaurantService.BLL.DTOs.RestaurantDocument;

public record RestaurantDocumentDto(
    Guid Id,
    string Type,
    string FileUrl,
    string Status,
    string? RejectionReason
);
