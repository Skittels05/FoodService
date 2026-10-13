using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Services.Interfaces;

public interface ICourierService
{
    Task GoOnlineAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default);
    Task GoOfflineAsync(Guid courierId, CancellationToken cancellationToken = default);
    Task<bool> UpdateLocationAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default);
}
