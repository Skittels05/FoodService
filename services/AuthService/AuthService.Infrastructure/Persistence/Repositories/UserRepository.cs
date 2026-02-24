using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext context)
    : GenericRepository<User>(context), IUserRepository
{
    public async Task<User?> GetByAuth0IdAsync(string auth0Id, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Auth0Id == auth0Id, cancellationToken);
    }
}
