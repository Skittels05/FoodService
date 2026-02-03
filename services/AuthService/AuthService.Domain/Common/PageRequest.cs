using AuthService.Domain.Constants;

namespace AuthService.Domain.Common;

public record PageRequest(
    int PageNumber = PaginationConstants.DefaultPageNumber,
    int PageSize = PaginationConstants.DefaultPageSize,
    string? SortBy = null,
    string? SortOrder = null
);
