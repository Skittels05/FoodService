using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Exceptions;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.BLL.Services;

public class MenuItemService(
    IMenuItemRepository menuItemRepository,
    IRestaurantRepository restaurantRepository,
    IMappingService mappingService) : IMenuItemService
{
    public async Task<PagedList<MenuItemDto>> GetAllByRestaurantIdAsync(Guid restaurantId, PageRequest request, CancellationToken cancellationToken = default)
    {
        var pagedMenuItems = await menuItemRepository.GetAllByRestaurantIdAsync(restaurantId, request, cancellationToken);
        return mappingService.MapPagedList<MenuItem, MenuItemDto>(pagedMenuItems);
    }

    public async Task<Guid> CreateAsync(CreateMenuItemDto dto, CancellationToken cancellationToken = default)
    {
        _ = await restaurantRepository.GetByIdAsync(dto.RestaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), dto.RestaurantId);

        var menuItem = mappingService.Map<CreateMenuItemDto, MenuItem>(dto);
        menuItem.RestaurantId = dto.RestaurantId;

        await menuItemRepository.AddAsync(menuItem, cancellationToken);
        return menuItem.Id;
    }

    public async Task UpdateAsync(UpdateMenuItemDto dto, CancellationToken cancellationToken = default)
    {
        var menuItem = await menuItemRepository.GetByIdAsync(dto.Id, cancellationToken, true)
            ?? throw new NotFoundException(nameof(MenuItem), dto.Id);

        menuItem.Name = dto.Name;
        menuItem.Price = dto.Price;
        menuItem.IsActive = dto.IsActive;

        await menuItemRepository.UpdateAsync(menuItem, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var isDeleted = await menuItemRepository.DeleteAsync(id, cancellationToken);
        
        if (!isDeleted)
            throw new NotFoundException(nameof(MenuItem), id);
    }
}
