using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain.Interfaces;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    UserManager<User> UserManager { get; }
    ICourierRepository CourierRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync();
}
