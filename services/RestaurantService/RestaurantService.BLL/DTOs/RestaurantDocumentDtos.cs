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
    DocumentType Type,
    string FileUrl
);

public record ReplaceRestaurantDocumentDto(
    string NewFileUrl
);

public record RejectRestaurantDocumentDto(
    string Reason
);
