using RestaurantService.API.RequestModels;
using RestaurantService.BLL.DTOs;

namespace RestaurantService.API.Mappers;

public static class RestaurantDocumentMappingProfile
{
    public static AddRestaurantDocumentDto ToDto(this AddRestaurantDocumentRequest request, Guid restaurantId)
    {
        return new AddRestaurantDocumentDto(restaurantId, request.Type, request.FileUrl);
    }

    public static ReplaceRestaurantDocumentDto ToDto(this ReplaceRestaurantDocumentRequest request, Guid documentId)
    {
        return new ReplaceRestaurantDocumentDto(documentId, request.NewFileUrl);
    }

    public static RejectRestaurantDocumentDto ToDto(this RejectRestaurantDocumentRequest request, Guid documentId)
    {
        return new RejectRestaurantDocumentDto(documentId, request.Reason);
    }
}
