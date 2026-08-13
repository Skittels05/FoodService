using DeliveryService.BLL.Common;
using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseModel
{
    Task<TEntity?> GetByIdAsync(Guid id, bool trackChanges = false, CancellationToken cancellationToken = default);
    Task<PagedList<TEntity>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default);
    void Add(TEntity entity);
}
