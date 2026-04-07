using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Extensions.Sorting;

public static class LocationSortingExtensions
{
    public static IQueryable<Location> ApplyLocationSorting(
        this IQueryable<Location> query, 
        string? sortBy, 
        SortOrder sortOrder)
    {
        if (Enum.TryParse<LocationSortBy>(sortBy, true, out var sortField))
        {
            return sortField switch
            {
                LocationSortBy.Address => sortOrder == SortOrder.Desc 
                    ? query.OrderByDescending(l => l.Address) : query.OrderBy(l => l.Address),
                
                LocationSortBy.IsAcceptingOrders => sortOrder == SortOrder.Desc 
                    ? query.OrderByDescending(l => l.IsAcceptingOrders) : query.OrderBy(l => l.IsAcceptingOrders),
                
                _ => sortOrder == SortOrder.Desc 
                    ? query.OrderByDescending(l => l.CreatedAt) : query.OrderBy(l => l.CreatedAt)
            };
        }

        return sortOrder == SortOrder.Desc 
            ? query.OrderByDescending(l => l.CreatedAt) 
            : query.OrderBy(l => l.CreatedAt);
    }
}
