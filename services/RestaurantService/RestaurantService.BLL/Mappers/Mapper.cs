using RestaurantService.BLL.DTOs;
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
            RestaurantId = Guid.Empty,
            Type = input.Type,
            FileUrl = input.FileUrl,
            Status = VerificationStatus.Pending,
            RejectionReason = null
        };
    }
}

public class MenuItemToDtoMapper : IMapper<MenuItem, MenuItemDto>
{
    public MenuItemDto Map(MenuItem input)
    {
        return new MenuItemDto(
            Id: input.Id,
            RestaurantId: input.RestaurantId,
            Name: input.Name,
            Price: input.Price,
            IsActive: input.IsActive
        );
    }
}

public class CreateMenuItemDtoToEntityMapper : IMapper<CreateMenuItemDto, MenuItem>
{
    public MenuItem Map(CreateMenuItemDto input)
    {
        return new MenuItem
        {
            RestaurantId = Guid.Empty,
            Name = input.Name,
            Price = input.Price,
            IsActive = input.IsActive
        };
    }
}

public class StopListItemToDtoMapper : IMapper<StopListItem, StopListItemDto>
{
    public StopListItemDto Map(StopListItem input)
    {
        return new StopListItemDto(
            Id: input.Id,
            LocationId: input.LocationId,
            MenuItemId: input.MenuItemId,
            Reason: input.Reason.ToString(),
            Description: input.Description
        );
    }
}

public class AddStopListItemDtoToEntityMapper : IMapper<AddStopListItemDto, StopListItem>
{
    public StopListItem Map(AddStopListItemDto input)
    {
        return new StopListItem
        {
            LocationId = Guid.Empty,
            MenuItemId = input.MenuItemId,
            Reason = input.Reason,
            Description = input.Description
        };
    }
}

public class LocationToDtoMapper(IMapper<StopListItem, StopListItemDto> stopListMapper)
    : IMapper<Location, LocationDto>
{
    public LocationDto Map(Location input)
    {
        return new LocationDto(
            Id: input.Id,
            RestaurantId: input.RestaurantId,
            Address: input.Address,
            Latitude: input.Latitude,
            Longitude: input.Longitude,
            IsAcceptingOrders: input.IsAcceptingOrders,
            StopList: input.StopList?.Select(stopListMapper.Map).ToList() ?? []
        );
    }
}

public class CreateLocationDtoToEntityMapper : IMapper<CreateLocationDto, Location>
{
    public Location Map(CreateLocationDto input)
    {
        return new Location
        {
            RestaurantId = Guid.Empty,
            Address = input.Address,
            Latitude = input.Latitude,
            Longitude = input.Longitude,
            IsAcceptingOrders = input.IsAcceptingOrders
        };
    }
}
