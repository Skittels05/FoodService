using RestaurantService.BLL.DTOs.Restaurant;
using RestaurantService.BLL.DTOs.RestaurantDocument;
using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Mappers;

public class RestaurantToDtoMapper(IMapper<RestaurantDocument, RestaurantDocumentDto> documentMapper)
    : IMapper<Restaurant, RestaurantDto>
{
    public RestaurantDto Map(Restaurant input)
    {
        return new RestaurantDto(
            Id: input.Id,
            Name: input.Name,
            IsVerified: input.IsVerified,
            IsActive: input.IsActive,
            Documents: input.Documents?.Select(documentMapper.Map).ToList() ?? []
        );
    }
}

public class CreateRestaurantDtoToEntityMapper : IMapper<CreateRestaurantDto, Restaurant>
{
    public Restaurant Map(CreateRestaurantDto input)
    {
        return new Restaurant
        {
            Name = input.Name,
            IsVerified = false,
            IsActive = false
        };
    }
}

public class RestaurantDocumentToDtoMapper : IMapper<RestaurantDocument, RestaurantDocumentDto>
{
    public RestaurantDocumentDto Map(RestaurantDocument input)
    {
        return new RestaurantDocumentDto(
            Id: input.Id,
            Type: input.Type.ToString(),
            FileUrl: input.FileUrl,
            Status: input.Status.ToString(),
            RejectionReason: input.RejectionReason
        );
    }
}

public class AddRestaurantDocumentDtoToEntityMapper : IMapper<AddRestaurantDocumentDto, RestaurantDocument>
{
    public RestaurantDocument Map(AddRestaurantDocumentDto input)
    {
        return new RestaurantDocument
        {
            Type = input.Type,
            FileUrl = input.FileUrl,
            Status = VerificationStatus.Pending,
            RejectionReason = null
        };
    }
}
