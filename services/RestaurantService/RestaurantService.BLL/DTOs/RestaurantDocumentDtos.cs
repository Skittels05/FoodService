using RestaurantService.BLL.Enums;

namespace RestaurantService.BLL.DTOs;

public record RestaurantDocumentDto(
    Guid Id,
    string Type,
    string FileUrl,
    string Status,
    string? RejectionReason
);

public record AddRestaurantDocumentDto(
    Guid RestaurantId,
    DocumentType Type,
    string FileUrl
);

public record ReplaceRestaurantDocumentDto(
    Guid Id,
    string NewFileUrl
);

public record RejectRestaurantDocumentDto(
    Guid Id,
    string Reason
);
