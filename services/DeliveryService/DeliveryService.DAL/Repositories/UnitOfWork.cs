using DeliveryService.DAL.Persistence;
using DeliveryService.BLL.Repositories.Interfaces;

namespace DeliveryService.DAL.Repositories;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
