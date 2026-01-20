using AuthService.Domain.Entities;

namespace AuthService.Domain.Interfaces.Repositories
{
    public interface IRefreshTokenRepository: IGenericRepository<RefreshToken>
    {
    }
}
