using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Models;

namespace RestaurantService.DAL.Extensions.Sorting;

public static class MenuItemSortingExtensions
{
    public static IQueryable<MenuItem> ApplyMenuItemSorting(
        this IQueryable<MenuItem> query, 
        string? sortBy, 
        SortOrder sortOrder)
    {
        if (Enum.TryParse<MenuItemSortBy>(sortBy, true, out var sortField))
        {
            return sortField switch
            {
                MenuItemSortBy.Name => sortOrder == SortOrder.Desc 
                    ? query.OrderByDescending(m => m.Name) : query.OrderBy(m => m.Name),
                
                MenuItemSortBy.Price => sortOrder == SortOrder.Desc 
                    ? query.OrderByDescending(m => m.Price) : query.OrderBy(m => m.Price),
                
                MenuItemSortBy.IsActive => sortOrder == SortOrder.Desc 
                    ? query.OrderByDescending(m => m.IsActive) : query.OrderBy(m => m.IsActive),
                
                _ => sortOrder == SortOrder.Desc 
                    ? query.OrderByDescending(m => m.CreatedAt) : query.OrderBy(m => m.CreatedAt)
            };
        }

        return sortOrder == SortOrder.Desc 
            ? query.OrderByDescending(m => m.CreatedAt) 
            : query.OrderBy(m => m.CreatedAt);
    }
}
