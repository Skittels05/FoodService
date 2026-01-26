using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces.Repositories;

public interface ICustomerRepository: IGenericRepository<Customer>
{
    Task<Customer> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
