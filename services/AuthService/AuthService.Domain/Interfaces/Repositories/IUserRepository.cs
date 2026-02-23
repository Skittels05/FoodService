using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByAuth0IdAsync(string auth0Id, CancellationToken cancellationToken);
}
