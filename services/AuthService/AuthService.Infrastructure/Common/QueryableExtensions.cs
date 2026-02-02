using AuthService.Domain.Common;
using AuthService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace AuthService.Infrastructure.Common;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, string? sortBy, string? sortOrder)
        where T : class, IEntityBase
    {
        if (string.IsNullOrWhiteSpace(sortBy))
            return query.OrderByDescending(e => e.CreatedAt);
        var prop = typeof(T).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (prop == null)
            return query.OrderByDescending(e => e.CreatedAt);
        var param = Expression.Parameter(typeof(T), "x");
        var lambda = Expression.Lambda(Expression.Property(param, prop), param);
        var method = sortOrder?.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";
        var result = Expression.Call(
            typeof(Queryable),
            method,
            new[] { typeof(T), prop.PropertyType },
            query.Expression,
            Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(result);
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
