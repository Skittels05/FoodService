using DeliveryService.BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.DAL.Outbox;

public static class OutboxQueries
{
    public static IQueryable<OutboxMessage> GetPendingForUpdate(
        this DbSet<OutboxMessage> dbSet,
        int batchSize)
    {
        return dbSet.FromSqlRaw(@"
            SELECT * 
            FROM ""OutboxMessages"" 
            WHERE ""ProcessedOn"" IS NULL
            ORDER BY ""OccurredOn"", ""Id""
            LIMIT {0}
            FOR UPDATE SKIP LOCKED", 
            batchSize);
    }
}
