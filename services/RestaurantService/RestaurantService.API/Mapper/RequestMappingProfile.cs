using RestaurantService.API.RequestModels;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Mappers.Interfaces;

namespace RestaurantService.API.Mappers;

public class CreateRestaurantRequestMapper : IMapper<CreateRestaurantRequest, CreateRestaurantDto>
{
    public CreateRestaurantDto Map(CreateRestaurantRequest input) => new(input.Name);
}

public class UpdateRestaurantRequestMapper : IMapper<UpdateRestaurantRequest, UpdateRestaurantDto>
{
    public UpdateRestaurantDto Map(UpdateRestaurantRequest input) => new(input.Name);
}

public class CreateLocationRequestMapper : IMapper<CreateLocationRequest, CreateLocationDto>
{
    public CreateLocationDto Map(CreateLocationRequest input) => 
        new(input.Address, input.Latitude, input.Longitude, input.IsAcceptingOrders);
}

public class UpdateLocationRequestMapper : IMapper<UpdateLocationRequest, UpdateLocationDto>
{
    public UpdateLocationDto Map(UpdateLocationRequest input) => 
        new(input.Address, input.Latitude, input.Longitude, input.IsAcceptingOrders);
}

public class CreateMenuItemRequestMapper : IMapper<CreateMenuItemRequest, CreateMenuItemDto>
{
    public CreateMenuItemDto Map(CreateMenuItemRequest input) => 
        new(input.Name, input.Price, input.IsActive);
}

public class UpdateMenuItemRequestMapper : IMapper<UpdateMenuItemRequest, UpdateMenuItemDto>
{
    public UpdateMenuItemDto Map(UpdateMenuItemRequest input) => 
        new(input.Name, input.Price, input.IsActive);
}

public class AddDocumentRequestMapper : IMapper<AddRestaurantDocumentRequest, AddRestaurantDocumentDto>
{
    public AddRestaurantDocumentDto Map(AddRestaurantDocumentRequest input) => 
        new(input.Type, input.FileUrl);
}

public class ReplaceDocumentRequestMapper : IMapper<ReplaceRestaurantDocumentRequest, ReplaceRestaurantDocumentDto>
{
    public ReplaceRestaurantDocumentDto Map(ReplaceRestaurantDocumentRequest input) => 
        new(input.NewFileUrl);
}

public class RejectDocumentRequestMapper : IMapper<RejectRestaurantDocumentRequest, RejectRestaurantDocumentDto>
{
    public RejectRestaurantDocumentDto Map(RejectRestaurantDocumentRequest input) => 
        new(input.Reason);
}

public class AddStopListItemRequestMapper : IMapper<AddStopListItemRequest, AddStopListItemDto>
{
    public AddStopListItemDto Map(AddStopListItemRequest input) => 
        new(input.MenuItemId, input.Reason, input.Description);
}
