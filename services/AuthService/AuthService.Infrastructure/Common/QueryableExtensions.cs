using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq.Dynamic.Core;

namespace AuthService.Infrastructure.Common;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplySorting<T>(
        this IQueryable<T> query,
        string? sortBy,
        string? sortOrder)
        where T : IEntityBase
    {
        if (string.IsNullOrWhiteSpace(sortBy))
        {
            return query.OrderByDescending(e => e.CreatedAt);
        }
        var propertyInfo = typeof(T).GetProperty(
            sortBy.Trim(),
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (propertyInfo is null)
        {
            return query.OrderByDescending(e => e.CreatedAt);
        }
        var direction = sortOrder?.ToLower() == "desc" ? "descending" : "ascending";
        var orderQuery = $"{propertyInfo.Name} {direction}";
        return query.OrderBy(orderQuery);
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = await source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
