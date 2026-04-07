using Microsoft.EntityFrameworkCore;
using RestaurantService.BLL.Common;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.DAL.Extensions;
using RestaurantService.DAL.Extensions.Sorting;
using RestaurantService.DAL.Persistence.Repositories;

namespace RestaurantService.DAL.Repositories;

public class MenuItemRepository(RestaurantDbContext context)
    : GenericRepository<MenuItem>(context), IMenuItemRepository
{
    public async Task<PagedList<MenuItem>> GetAllByRestaurantIdAsync(Guid restaurantId, PageRequest request, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(m => m.RestaurantId == restaurantId)
            .AsNoTracking()
            .ApplyMenuItemSorting(request.SortBy, request.SortOrder)
            .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
