using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AuthService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class RestaurantManagerRepository(ApplicationDbContext context)
    : GenericRepository<RestaurantManager>(context), IRestaurantManagerRepository
{
    public async Task<PagedList<RestaurantManager>> GetByRestaurantIdAsync(Guid restaurantId, int page, int pageSize, bool trackChanges, CancellationToken cancellationToken)
    {
        return await (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .Where(rm => rm.ManagedRestaurantId == restaurantId)
            .OrderBy(rm => rm.CreatedAt)
            .ToPagedListAsync(page, pageSize, cancellationToken);
    }

    public async Task<RestaurantManager?> GetByUserIdAsync(Guid userId, bool trackChanges, CancellationToken cancellationToken)
    {
        return await (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .FirstOrDefaultAsync(rm => rm.UserId == userId, cancellationToken);
    }
}
