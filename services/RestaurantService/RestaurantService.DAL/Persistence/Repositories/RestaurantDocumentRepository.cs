using Microsoft.EntityFrameworkCore;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.DAL.Persistence.Repositories;

namespace RestaurantService.DAL.Repositories;

public class RestaurantDocumentRepository(RestaurantDbContext context) 
    : GenericRepository<RestaurantDocument>(context), IRestaurantDocumentRepository
{
    public async Task<IEnumerable<RestaurantDocument>> GetByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(d => d.RestaurantId == restaurantId)
            .ToListAsync(cancellationToken);
    }
}
