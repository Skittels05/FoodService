using AuthService.Domain.Interfaces.Repositories;

namespace AuthService.Domain.Interfaces;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IUserRepository UserRepository { get; }
    ICourierRepository CourierRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IRestaurantManagerRepository RestaurantManagerRepository { get; }
    ICustomerAddressRepository CustomerAddressRepository { get; }

    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync();
}
