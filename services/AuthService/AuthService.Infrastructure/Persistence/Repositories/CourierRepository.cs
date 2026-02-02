using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AuthService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class CourierRepository(ApplicationDbContext context)
    : GenericRepository<Courier>(context), ICourierRepository
{
    public async Task<Courier> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }

    public async Task<PagedList<Courier>> GetPendingCouriersAsync(
    int page, int pageSize, string? sortBy, string? sortOrder, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(c => c.IsVerified == false)
            .ApplySorting(sortBy, sortOrder)
            .ToPagedListAsync(page, pageSize, cancellationToken);
    }
}
