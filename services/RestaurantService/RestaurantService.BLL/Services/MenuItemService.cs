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
    public async Task<IEnumerable<MenuItemDto>> GetAllByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default)
    {
        var menuItems = await menuItemRepository.GetAllByRestaurantIdAsync(restaurantId, cancellationToken);

        return menuItems.Select(mappingService.Map<MenuItem, MenuItemDto>);
    }

    public async Task<Guid> CreateAsync(Guid restaurantId, CreateMenuItemDto dto, CancellationToken cancellationToken = default)
    {
        _ = await restaurantRepository.GetByIdAsync(restaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        var menuItem = mappingService.Map<CreateMenuItemDto, MenuItem>(dto);
        menuItem.RestaurantId = restaurantId;

        await menuItemRepository.AddAsync(menuItem, cancellationToken);
        return menuItem.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateMenuItemDto dto, CancellationToken cancellationToken = default)
    {
        var menuItem = await menuItemRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(MenuItem), id);

        menuItem.Name = dto.Name;
        menuItem.Price = dto.Price;
        menuItem.IsActive = dto.IsActive;

        await menuItemRepository.UpdateAsync(menuItem, cancellationToken);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => menuItemRepository.DeleteAsync(id, cancellationToken);
}
