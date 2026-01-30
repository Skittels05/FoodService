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

    public async Task<User?> GetByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges ? _userManager.Users : _userManager.Users.AsNoTracking();
        return await query.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<PagedList<User>> GetAllAsync(int page, int pageSize, bool trackChanges, CancellationToken cancellationToken)
    {
        var query = trackChanges ? _userManager.Users : _userManager.Users.AsNoTracking();

        return await query
            .OrderBy(u => u.Email)
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
