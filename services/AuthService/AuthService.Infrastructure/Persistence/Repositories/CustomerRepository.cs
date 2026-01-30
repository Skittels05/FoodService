using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class CustomerRepository(ApplicationDbContext context)
    : GenericRepository<Customer>(context), ICustomerRepository
{
    public async Task<Customer?> GetByUserIdAsync(Guid userId, bool trackChanges, CancellationToken cancellationToken)
    {
        return await (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }
}
