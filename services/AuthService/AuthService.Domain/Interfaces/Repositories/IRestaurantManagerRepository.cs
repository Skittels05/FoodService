using AuthService.Domain.Common;
using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces.Repositories;

public interface IRestaurantManagerRepository : IGenericRepository<RestaurantManager>
{
    Task<PagedList<RestaurantManager>> GetByRestaurantIdAsync(
        Guid restaurantId, 
        int page, 
        int pageSize,
        string? sortBy,
        string? sortOrder,
        CancellationToken cancellationToken);
    Task<RestaurantManager?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
