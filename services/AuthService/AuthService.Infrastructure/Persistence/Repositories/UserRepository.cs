using AuthService.Domain.Common;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AuthService.Infrastructure.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class UserRepository(UserManager<User> userManager) : IUserRepository
{
    private readonly UserManager<User> _userManager = userManager;

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<PagedList<User>> GetAllAsync(
    int page, int pageSize, string? sortBy, string? sortOrder, CancellationToken cancellationToken)
    {
        return await _userManager.Users
            .ApplySorting(sortBy, sortOrder)
            .ToPagedListAsync(page, pageSize, cancellationToken);
    }

    public async Task<IdentityResult> CreateAsync(User user, string password, CancellationToken cancellationToken)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        return await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        return await _userManager.DeleteAsync(user);
    }
}
