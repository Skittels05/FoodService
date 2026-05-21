using DeliveryService.BLL.Enums;

namespace RestaurantService.BLL.Common;

public record PageRequest(
    int PageNumber = 1,
    int PageSize = 10,
    string? SortBy = null,
    SortOrder SortOrder = SortOrder.Asc
);
