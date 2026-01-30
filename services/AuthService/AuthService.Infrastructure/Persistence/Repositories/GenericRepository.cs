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

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        return trackChanges
            ? await _dbSet.FindAsync([id], cancellationToken)
            : await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<PagedList<TEntity>> GetAllAsync(int page, int pageSize, bool trackChanges, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = trackChanges
            ? _dbSet
            : _dbSet.AsNoTracking();

        return await query
            .OrderByDescending(x => x.CreatedAt)
            .ToPagedListAsync(page, pageSize, cancellationToken);
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

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }
}
