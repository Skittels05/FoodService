using DeliveryService.DAL.Persistence;
using DeliveryService.BLL.Common;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.DAL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.DAL.Repositories;

public class OrderRepository(ApplicationDbContext dbContext) 
    : GenericRepository<Order>(dbContext), IOrderRepository
{
    public async Task<Order?> GetByIdWithPaymentsAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(o => o.Payments)
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
    }

    public async Task<PagedList<Order>> GetByCustomerAsync(Guid userId, PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false)
    {
        var query = trackChanges ? DbSet : DbSet.AsNoTracking();

        return await query
            .Where(o => o.CustomerId == userId)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }

    public async Task<PagedList<Order>> GetByCourierAsync(Guid courierId, PageRequest request, CancellationToken cancellationToken = default, bool trackChanges = false)
    {
        var query = trackChanges ? DbSet : DbSet.AsNoTracking();

        return await query
            .Where(o => o.CourierId == courierId)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
