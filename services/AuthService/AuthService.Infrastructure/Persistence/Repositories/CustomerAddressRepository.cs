using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AuthService.Infrastructure.Common;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class CustomerAddressRepository(ApplicationDbContext context)
    : GenericRepository<CustomerAddress>(context), ICustomerAddressRepository
{
    public async Task<PagedList<CustomerAddress>> GetByCustomerIdAsync(Guid customerId, PageRequest request, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(a => a.CustomerId == customerId)
            .ApplySorting(request.SortBy, request.SortOrder)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
