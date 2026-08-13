using DeliveryService.DAL.Persistence;
using DeliveryService.BLL.Common;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.DAL.Repositories;

public class GenericRepository<TEntity>(ApplicationDbContext dbContext) 
    : IGenericRepository<TEntity> where TEntity : BaseModel
{
    protected readonly ApplicationDbContext DbContext = dbContext;
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, bool trackChanges = false)
    {
        var query = trackChanges ? DbSet : DbSet.AsNoTracking();
        return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<PagedList<TEntity>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false)
    {
        var query = trackChanges ? DbSet : DbSet.AsNoTracking();
        return await query.ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }

    public void Add(TEntity entity)
    {
        DbSet.Add(entity);
    }
}
