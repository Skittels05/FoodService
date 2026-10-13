using DeliveryService.BLL.Models;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.DAL.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.DAL.Repositories;

public class CourierStateRepository(ApplicationDbContext dbContext) : ICourierStateRepository
{
    private readonly DbSet<CourierState> _dbSet = dbContext.Set<CourierState>();

    public async Task<CourierState?> GetByIdAsync(Guid courierId, bool trackChanges = false, CancellationToken cancellationToken = default)
    {
        var query = trackChanges ? _dbSet : _dbSet.AsNoTracking();
        return await query.FirstOrDefaultAsync(c => c.CourierId == courierId, cancellationToken);
    }

    public void Add(CourierState courierState)
    {
        _dbSet.Add(courierState);
    }
}
