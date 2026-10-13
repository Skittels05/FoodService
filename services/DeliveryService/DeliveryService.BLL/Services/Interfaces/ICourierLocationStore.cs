using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Services.Interfaces;

public interface ICourierLocationStore
{
    Task SetAvailableAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default);
    Task SetDeliveringAsync(Guid courierId, CourierLocation? location = null, CancellationToken cancellationToken = default);
    Task SetOfflineAsync(Guid courierId, CancellationToken cancellationToken = default);
    Task<LocationUpdateResult> UpdateLocationAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default);
    Task<CourierLocation?> GetLocationAsync(Guid courierId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<NearbyCourier>> FindNearestAvailableAsync(
        double latitude,
        double longitude,
        double radiusKm,
        int count,
        CancellationToken cancellationToken = default);
}
