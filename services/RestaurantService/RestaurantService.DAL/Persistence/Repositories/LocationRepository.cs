using Microsoft.EntityFrameworkCore;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;

namespace RestaurantService.DAL.Persistence.Repositories;

public class LocationRepository(RestaurantDbContext context)
    : GenericRepository<Location>(context), ILocationRepository
{
    public async Task<IEnumerable<Location>> GetAllByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(l => l.RestaurantId == restaurantId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Location>> GetByIdsWithRestaurantAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(l => l.Restaurant)
            .Where(l => ids.Contains(l.Id))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
