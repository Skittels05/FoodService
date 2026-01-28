using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PagedList<User>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken);
    Task<IdentityResult> CreateAsync(User user, string password, CancellationToken cancellationToken);
    Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken);
    Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken);
}
