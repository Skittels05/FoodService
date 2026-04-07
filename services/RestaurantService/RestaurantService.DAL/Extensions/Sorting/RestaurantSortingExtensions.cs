using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Extensions.Sorting;

public static class RestaurantSortingExtensions
{
    public static IQueryable<Restaurant> ApplyRestaurantSorting(
        this IQueryable<Restaurant> query, 
        string? sortBy, 
        SortOrder sortDirection)
    {
        if (Enum.TryParse<RestaurantSortBy>(sortBy, true, out var sortField))
        {
            return sortField switch
            {
                RestaurantSortBy.Name => sortDirection == SortOrder.Desc 
                    ? query.OrderByDescending(r => r.Name) : query.OrderBy(r => r.Name),
                
                RestaurantSortBy.IsActive => sortDirection == SortOrder.Desc 
                    ? query.OrderByDescending(r => r.IsActive) : query.OrderBy(r => r.IsActive),
                
                _ => sortDirection == SortOrder.Desc 
                    ? query.OrderByDescending(r => r.CreatedAt) : query.OrderBy(r => r.CreatedAt)
            };
        }
        
        return sortDirection == SortOrder.Desc 
            ? query.OrderByDescending(r => r.CreatedAt) 
            : query.OrderBy(r => r.CreatedAt);
    }
}
