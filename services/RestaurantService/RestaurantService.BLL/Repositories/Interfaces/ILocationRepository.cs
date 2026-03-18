using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Repositories.Interfaces;

public interface ILocationRepository : IGenericRepository<Location>
{
    Task<IEnumerable<Location>> GetAllByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default);
}
