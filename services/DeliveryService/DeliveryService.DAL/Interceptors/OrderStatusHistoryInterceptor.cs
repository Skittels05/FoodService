namespace DeliveryService.DAL.Interceptors;

using DeliveryService.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class OrderStatusHistoryInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        CreateStatusHistoryEntries(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        CreateStatusHistoryEntries(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    private static void CreateStatusHistoryEntries(DbContext? context)
    {
        if (context is null) return;

        var orderEntries = context.ChangeTracker
            .Entries<Order>()
            .Where(e => e.State == EntityState.Added ||
                        (e.State == EntityState.Modified && e.Property(o => o.Status).IsModified));

        foreach (var entry in orderEntries)
        {
            var order = entry.Entity;

            var historyEntry = new OrderStatusHistory
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                OrderStatus = order.Status,
                ChangedBy = null
            };

            context.Set<OrderStatusHistory>().Add(historyEntry);
        }
    }
}
