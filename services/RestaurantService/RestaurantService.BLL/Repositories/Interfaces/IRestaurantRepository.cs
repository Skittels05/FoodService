using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Repositories.Interfaces;

public interface IRestaurantRepository : IGenericRepository<Restaurant>
{
    Task<Restaurant?> GetByIdWithDocumentsAsync(Guid id, CancellationToken cancellationToken = default, bool trackChanges = false);
}
