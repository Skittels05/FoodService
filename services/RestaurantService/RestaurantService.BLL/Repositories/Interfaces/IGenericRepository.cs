using RestaurantService.BLL.Models;

namespace RestaurantService.BLL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseModel
{
    Task<IEnumerable<TEntity>> GetAllAsync( CancellationToken cancellationToken = default, bool trackChanges = false);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool trackChanges = false);
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
