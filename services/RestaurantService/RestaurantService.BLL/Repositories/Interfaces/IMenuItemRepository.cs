using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Repositories.Interfaces;

public interface IMenuItemRepository : IGenericRepository<MenuItem>
{
    Task<IEnumerable<MenuItem>> GetAllByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default);
}
