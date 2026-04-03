using RestaurantService.BLL.Common;
using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Repositories.Interfaces;

public interface IMenuItemRepository : IGenericRepository<MenuItem>
{
    Task<PagedList<MenuItem>> GetAllByRestaurantIdAsync(Guid restaurantId, PageRequest request, CancellationToken cancellationToken = default);
}
