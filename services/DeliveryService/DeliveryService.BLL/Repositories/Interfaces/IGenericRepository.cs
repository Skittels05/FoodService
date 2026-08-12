using DeliveryService.BLL.Common;
using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseModel
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool trackChanges = false);
    Task<PagedList<TEntity>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}
