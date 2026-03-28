using Microsoft.EntityFrameworkCore;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.DAL.Persistence.Repositories;

namespace RestaurantService.DAL.Repositories;

public class MenuItemRepository(RestaurantDbContext context)
    : GenericRepository<MenuItem>(context), IMenuItemRepository
{
    public async Task<IEnumerable<MenuItem>> GetAllByRestaurantIdAsync(Guid restaurantId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(m => m.RestaurantId == restaurantId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
