using AuthService.Application.DTO.Users;
using AuthService.Domain.Common;
using AuthService.Domain.Constants;
using MediatR;

namespace AuthService.Application.CQRS.Users.Queries;

public record GetAllUsersQuery(
    int Page = PaginationConstants.DefaultPageNumber,
    int PageSize = PaginationConstants.DefaultPageSize
) : IRequest<PagedList<UserAccountDto>>;
