using AuthService.Domain.Entities;
using System.Linq.Expressions;

namespace AuthService.Domain.Interfaces.Repositories;

public interface ICourierRepository: IGenericRepository<Courier>
{
    Task<Courier> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<Courier>> GetPendingCouriersAsync(CancellationToken cancellationToken);
}
