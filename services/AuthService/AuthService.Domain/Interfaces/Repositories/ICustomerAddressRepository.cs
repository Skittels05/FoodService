using AuthService.Domain.Common;
using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces.Repositories;

public interface ICustomerAddressRepository : IGenericRepository<CustomerAddress>
{
    Task<PagedList<CustomerAddress>> GetByCustomerIdAsync(Guid customerId, int page, int pageSize, bool trackChanges, CancellationToken cancellationToken);
}
