using RestaurantService.BLL.DTOs.Restaurant;
using RestaurantService.BLL.DTOs.RestaurantDocument;
using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Mappers;

public static class Mapper
{

    public static RestaurantDto ToDto(this Restaurant restaurant)
    {
        return new RestaurantDto(
            Id: restaurant.Id,
            Name: restaurant.Name,
            IsVerified: restaurant.IsVerified,
            IsActive: restaurant.IsActive,
            Documents: restaurant.Documents?.Select(d => d.ToDto()).ToList() ?? new List<RestaurantDocumentDto>()
        );
    }

    public static Restaurant ToEntity(this CreateRestaurantDto dto)
    {
        return new Restaurant
        {
            Name = dto.Name,
            IsVerified = false,
            IsActive = false
        };
    }

    public static RestaurantDocumentDto ToDto(this RestaurantDocument document)
    {
        return new RestaurantDocumentDto(
            Id: document.Id,
            Type: document.Type.ToString(),
            FileUrl: document.FileUrl,
            Status: document.Status.ToString(),
            RejectionReason: document.RejectionReason
        );
    }

    public static RestaurantDocument ToEntity(this AddRestaurantDocumentDto dto, Guid restaurantId)
    {
        return new RestaurantDocument
        {
            RestaurantId = restaurantId,
            Type = dto.Type,
            FileUrl = dto.FileUrl,
            Status = VerificationStatus.Pending,
            RejectionReason = null
        };
    }

}
