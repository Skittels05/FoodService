using Microsoft.EntityFrameworkCore;
using RestaurantService.BLL.Common;
using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.DAL.Extensions;

namespace RestaurantService.DAL.Persistence.Repositories;

public class GenericRepository<TEntity>(RestaurantDbContext context)
    : IGenericRepository<TEntity> where TEntity : BaseModel
{
    protected readonly RestaurantDbContext DbContext = context;
    protected readonly DbSet<TEntity> DbSet = context.Set<TEntity>();

    public virtual async Task<PagedList<TEntity>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false)
    {
        var query = trackChanges ? DbSet : DbSet.AsNoTracking();

        query = request.SortOrder == SortOrder.Desc
            ? query.OrderByDescending(e => e.CreatedAt)
            : query.OrderBy(e => e.CreatedAt);

        return await query.ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool trackChanges = false)
    {
        return trackChanges
            ? await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken)
            : await DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var rowsAffected = await DbSet
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        return rowsAffected > 0;
    }
}
