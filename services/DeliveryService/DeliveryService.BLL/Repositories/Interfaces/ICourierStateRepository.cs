using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Repositories.Interfaces;

public interface ICourierStateRepository
{
    Task<CourierState?> GetByIdAsync(Guid courierId, CancellationToken cancellationToken = default, bool trackChanges = false);
    Task UpdateAsync(CourierState courierState, CancellationToken cancellationToken = default);
}
