using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Services.Interfaces;

public interface IMenuItemService
{
    Task<IEnumerable<MenuItemDto>> GetAllByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(Guid restaurantId, CreateMenuItemDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, UpdateMenuItemDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
