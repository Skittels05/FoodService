using RestaurantService.BLL.Enums;

namespace RestaurantService.API.RequestModels;

public record AddRestaurantDocumentRequest(
    DocumentType Type,
    string FileUrl
);

public record ReplaceRestaurantDocumentRequest(
    string NewFileUrl
);

public record RejectRestaurantDocumentRequest(
    string Reason
);
