using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class UnitOfWork(ApplicationDbContext context, UserManager<User> userManager) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<User> _userManager = userManager;
    private IDbContextTransaction? _currentTransaction;

    public IUserRepository UserRepository { get; } = new UserRepository(userManager);
    public ICourierRepository CourierRepository { get; } = new CourierRepository(context);
    public ICustomerRepository CustomerRepository { get; } = new CustomerRepository(context);
    public IRestaurantManagerRepository RestaurantManagerRepository { get; } = new RestaurantManagerRepository(context);
    public ICustomerAddressRepository CustomerAddressRepository { get; } = new CustomerAddressRepository(context);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (_currentTransaction is not null) return;
        _currentTransaction = await _context.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);

            if (_currentTransaction is not null)
            {
                await _currentTransaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_currentTransaction is not null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public void Dispose()
    {
        _currentTransaction?.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (_currentTransaction is not null)
        {
            await _currentTransaction.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
