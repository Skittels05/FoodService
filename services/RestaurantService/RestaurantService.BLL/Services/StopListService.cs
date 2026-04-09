using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Exceptions;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.BLL.Services;

public class StopListService(
    IGenericRepository<StopListItem> stopListRepository,
    IGenericRepository<Location> locationRepository,
    IGenericRepository<MenuItem> menuItemRepository,
    IMappingService mappingService) : IStopListService
{
    public async Task<Guid> AddItemAsync(AddStopListItemDto dto, CancellationToken cancellationToken = default)
    {
        _ = await locationRepository.GetByIdAsync(dto.LocationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Location), dto.LocationId);

        _ = await menuItemRepository.GetByIdAsync(dto.MenuItemId, cancellationToken)
            ?? throw new NotFoundException(nameof(MenuItem), dto.MenuItemId);

        var stopListItem = mappingService.Map<AddStopListItemDto, StopListItem>(dto);
        stopListItem.LocationId = dto.LocationId;

        await stopListRepository.AddAsync(stopListItem, cancellationToken);
        return stopListItem.Id;
    }

    public async Task RemoveItemAsync(Guid itemId, CancellationToken cancellationToken = default)
    {
        _ = await stopListRepository.GetByIdAsync(itemId, cancellationToken)
            ?? throw new NotFoundException(nameof(StopListItem), itemId);

        await stopListRepository.DeleteAsync(itemId, cancellationToken);
    }
}
