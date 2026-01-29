using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class CustomerRepository(ApplicationDbContext context)
    : GenericRepository<Customer>(context), ICustomerRepository
{
    public async Task<Customer> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.UserId == userId, cancellationToken);
    }
}
