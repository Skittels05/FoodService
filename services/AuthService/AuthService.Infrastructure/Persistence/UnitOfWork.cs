using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AuthService.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthService.Infrastructure.Persistence.Repositories;

public class UnitOfWork(ApplicationDbContext context, UserManager<User> userManager) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private readonly UserManager<User> _userManager = userManager;
    private IDbContextTransaction? _currentTransaction;
    private IUserRepository? _userRepository;
    private ICourierRepository? _courierRepository;
    private ICustomerRepository? _customerRepository;
    private IRestaurantManagerRepository? _restaurantManagerRepository;
    private ICustomerAddressRepository? _customerAddressRepository;

    public IUserRepository UserRepository =>
        _userRepository ??= new UserRepository(_userManager);

    public ICourierRepository CourierRepository =>
        _courierRepository ??= new CourierRepository(_context);

    public ICustomerRepository CustomerRepository =>
        _customerRepository ??= new CustomerRepository(_context);

    public IRestaurantManagerRepository RestaurantManagerRepository =>
        _restaurantManagerRepository ??= new RestaurantManagerRepository(_context);

    public ICustomerAddressRepository CustomerAddressRepository =>
        _customerAddressRepository ??= new CustomerAddressRepository(_context);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null) return;
        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);

            if (_currentTransaction != null)
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
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}
