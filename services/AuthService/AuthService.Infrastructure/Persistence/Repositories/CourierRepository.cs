using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AuthService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class CourierRepository(ApplicationDbContext context)
    : GenericRepository<Courier>(context), ICourierRepository
{
    public async Task<Courier?> GetByUserIdAsync(Guid userId, bool trackChanges, CancellationToken cancellationToken)
    {
        return await (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }

    public async Task<PagedList<Courier>> GetPendingCouriersAsync(int page, int pageSize, bool trackChanges, CancellationToken cancellationToken)
    {
        return await (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .Where(c => c.IsVerified == false)
            .OrderBy(c => c.CreatedAt)
            .ToPagedListAsync(page, pageSize, cancellationToken);
    }
}
