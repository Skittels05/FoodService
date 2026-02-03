using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Interfaces.Repositories;
using AuthService.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity>(ApplicationDbContext context) : IGenericRepository<TEntity>
    where TEntity : class, IEntityBase
{
    protected readonly ApplicationDbContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync([id], cancellationToken);
    }

    public virtual async Task<PagedList<TEntity>> GetAllAsync(PageRequest request, CancellationToken cancellationToken)
    {
        return await _dbSet
            .AsNoTracking()
            .ApplySorting(request.SortBy, request.SortOrder)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }

    public virtual Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Add(entity);
        return Task.FromResult(entity);
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        return Task.FromResult(entity);
    }

    public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var rowsAffected = await _dbSet
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        return rowsAffected > 0;
    }
}
