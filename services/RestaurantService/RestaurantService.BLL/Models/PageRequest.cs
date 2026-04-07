using RestaurantService.BLL.Enums;

namespace RestaurantService.BLL.Models;

public record PageRequest(
    int PageNumber = 1,
    int PageSize = 10,
    string? SortBy = null,
    SortOrder SortOrder = SortOrder.Asc
);
