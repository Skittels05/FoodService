using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AuthService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class RestaurantManagerRepository(ApplicationDbContext context)
    : GenericRepository<RestaurantManager>(context), IRestaurantManagerRepository
{
    public async Task<PagedList<RestaurantManager>> GetByRestaurantIdAsync(
    Guid restaurantId, int page, int pageSize, string? sortBy, string? sortOrder, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(rm => rm.ManagedRestaurantId == restaurantId)
            .ApplySorting(sortBy, sortOrder)
            .ToPagedListAsync(page, pageSize, cancellationToken);
    }

    public async Task<RestaurantManager?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(rm => rm.UserId == userId, cancellationToken);
    }
}
