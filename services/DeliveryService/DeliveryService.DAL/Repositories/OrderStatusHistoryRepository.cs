using DeliveryService.DAL.Persistence;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.DAL.Repositories;

public class OrderStatusHistoryRepository(ApplicationDbContext dbContext) 
    : GenericRepository<OrderStatusHistory>(dbContext), IOrderStatusHistoryRepository
{
    public async Task<List<OrderStatusHistory>> GetByOrderIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Where(h => h.OrderId == orderId)
            .OrderByDescending(h => h.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
