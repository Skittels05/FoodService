using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Repositories.Interfaces;

public interface ICourierStateRepository
{
    Task<CourierState?> GetByIdAsync(Guid courierId, bool trackChanges = false, CancellationToken cancellationToken = default);
    void Add(CourierState courierState);
}
