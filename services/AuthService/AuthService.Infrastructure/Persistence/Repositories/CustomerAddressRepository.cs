using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AuthService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class CustomerAddressRepository(ApplicationDbContext context)
    : GenericRepository<CustomerAddress>(context), ICustomerAddressRepository
{
    public async Task<PagedList<CustomerAddress>> GetByCustomerIdAsync(Guid customerId, int page, int pageSize, bool trackChanges, CancellationToken cancellationToken)
    {
        return await (trackChanges ? _dbSet : _dbSet.AsNoTracking())
            .Where(a => a.CustomerId == customerId)
            .OrderByDescending(a => a.CreatedAt)
            .ToPagedListAsync(page, pageSize, cancellationToken);
    }
}
