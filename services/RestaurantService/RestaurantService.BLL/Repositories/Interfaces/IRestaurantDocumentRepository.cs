using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Repositories.Interfaces;

public interface IRestaurantDocumentRepository : IGenericRepository<RestaurantDocument>
{
    Task<IEnumerable<RestaurantDocument>> GetByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default);
}
