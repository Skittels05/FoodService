using Microsoft.EntityFrameworkCore;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;

namespace RestaurantService.DAL.Persistence.Repositories;

public class RestaurantRepository(RestaurantDbContext context)
: GenericRepository<Restaurant>(context), IRestaurantRepository
{
    public Task<Restaurant?> GetByIdWithDocumentsAsync(Guid id, CancellationToken cancellationToken = default, bool trackChanges = false)
    {
        var query = DbSet.Include(r => r.Documents);

        return trackChanges
            ? query.FirstOrDefaultAsync(r => r.Id == id, cancellationToken)
            : query.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
}
