using Microsoft.EntityFrameworkCore;
using RestaurantService.BLL.Common;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.DAL.Extensions;
using RestaurantService.DAL.Extensions.Sorting;

namespace RestaurantService.DAL.Persistence.Repositories;

public class LocationRepository(RestaurantDbContext context)
    : GenericRepository<Location>(context), ILocationRepository
{
    public async Task<PagedList<Location>> GetAllByRestaurantIdAsync(Guid restaurantId, PageRequest request, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(l => l.RestaurantId == restaurantId)
            .AsNoTracking()
            .ApplyLocationSorting(request.SortBy, request.SortOrder)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
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
