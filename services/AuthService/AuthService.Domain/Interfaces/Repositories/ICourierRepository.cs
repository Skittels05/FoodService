using AuthService.Domain.Common;
using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces.Repositories;

public interface ICourierRepository: IGenericRepository<Courier>
{
    Task<Courier> GetByUserIdAsync(Guid userId, bool trackChanges, CancellationToken cancellationToken);
    Task<PagedList<Courier>> GetPendingCouriersAsync(int page, int pageSize, bool trackChanges, CancellationToken cancellationToken);
}
