using RestaurantService.BLL.Enums;

namespace RestaurantService.BLL.DTOs.RestaurantDocument;

public record AddRestaurantDocumentDto(DocumentType Type, string FileUrl);
