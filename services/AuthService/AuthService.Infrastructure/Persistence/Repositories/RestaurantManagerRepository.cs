using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AuthService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class RestaurantManagerRepository(ApplicationDbContext context)
    : GenericRepository<RestaurantManager>(context), IRestaurantManagerRepository
{
    public async Task<PagedList<RestaurantManager>> GetByRestaurantIdAsync(Guid restaurantId, bool? isVerified, PageRequest request, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(rm => rm.ManagedRestaurantId == restaurantId)
            .Where(rm => isVerified == null || rm.IsVerified == isVerified)
            .ApplySorting(request.SortBy, request.SortOrder)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }

    public async Task<RestaurantManager?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(rm => rm.UserId == userId, cancellationToken);
    }
    
    public async Task<RestaurantManager?> GetPendingByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken)
    {
        return await _dbSet.SingleOrDefaultAsync(
            x => x.ManagedRestaurantId == restaurantId && !x.IsVerified, 
            cancellationToken);
    }
}
