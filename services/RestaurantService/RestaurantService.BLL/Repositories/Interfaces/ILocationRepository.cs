using RestaurantService.BLL.Common;
using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Repositories.Interfaces;

public interface ILocationRepository : IGenericRepository<Location>
{
    Task<PagedList<Location>> GetAllByRestaurantIdAsync(Guid restaurantId, PageRequest request, CancellationToken cancellationToken = default);
    Task<IEnumerable<Location>> GetByIdsWithRestaurantAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
}
