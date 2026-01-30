using System.Linq.Expressions;
using AuthService.Domain.Common;

namespace AuthService.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : IEntityBase
    {
        Task<TEntity?> GetByIdAsync(Guid id,bool trackChanges, CancellationToken cancellationToken);
        Task<PagedList<TEntity>> GetAllAsync(int page, int pageSize, bool trackChanges, CancellationToken cancellationToken);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
