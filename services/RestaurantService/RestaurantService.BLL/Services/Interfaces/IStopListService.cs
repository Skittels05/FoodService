using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Services.Interfaces;

public interface IStopListService
{
    Task<Guid> AddItemAsync(AddStopListItemDto dto, CancellationToken cancellationToken = default);
    Task RemoveItemAsync(Guid itemId, CancellationToken cancellationToken = default);
}
