using DeliveryService.BLL.Enums;

namespace DeliveryService.BLL.Common;

public record PageRequest(
    int PageNumber = 1,
    int PageSize = 10,
    string? SortBy = null,
    SortOrder SortOrder = SortOrder.Asc
);
