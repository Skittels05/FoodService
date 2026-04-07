using Microsoft.EntityFrameworkCore;
using RestaurantService.BLL.Common;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.DAL.Extensions;
using RestaurantService.DAL.Extensions.Sorting;

namespace RestaurantService.DAL.Persistence.Repositories;

public class RestaurantRepository(RestaurantDbContext context)
: GenericRepository<Restaurant>(context), IRestaurantRepository
{
    public override async Task<PagedList<Restaurant>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false)
    {
        var query = trackChanges ? DbSet : DbSet.AsNoTracking();

        return await query
            .ApplyRestaurantSorting(request.SortBy, request.SortOrder)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
    public Task<Restaurant?> GetByIdWithDocumentsAsync(Guid id, CancellationToken cancellationToken = default, bool trackChanges = false)
    {
        var query = DbSet.Include(r => r.Documents);

        return trackChanges
            ? query.FirstOrDefaultAsync(r => r.Id == id, cancellationToken)
            : query.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}
