using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Services.Interfaces;

public interface IMenuItemService
{
    Task<PagedList<MenuItemDto>> GetAllByRestaurantIdAsync(Guid restaurantId, PageRequest request, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(CreateMenuItemDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateMenuItemDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
