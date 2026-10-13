using DeliveryService.BLL.Constants;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Exceptions;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.BLL.Services.Interfaces;

namespace DeliveryService.BLL.Services;

public class CourierService(
    ICourierStateRepository courierStateRepository,
    ICourierLocationStore courierLocationStore,
    IUnitOfWork unitOfWork,
    TimeProvider timeProvider) : ICourierService
{
    private static readonly TimeSpan MaxLocationAge =
        TimeSpan.FromSeconds(Constraints.CourierLocationMaxAgeInSeconds);

    public async Task GoOnlineAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default)
    {
        if (IsStale(location))
        {
            throw new StaleCourierLocationException(courierId);
        }

        var state = await courierStateRepository.GetByIdAsync(courierId, trackChanges: true, cancellationToken);

        if (state is null)
        {
            state = new CourierState { CourierId = courierId, Status = CourierStatus.Available };
            courierStateRepository.Add(state);
        }
        else if (state.Status == CourierStatus.Delivering)
        {
            await UpdateLocationAsync(courierId, location, cancellationToken);
            return;
        }

        state.Status = CourierStatus.Available;
        state.LastSeenAt = location.Timestamp;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await courierLocationStore.SetAvailableAsync(courierId, location, cancellationToken);
    }

    public async Task GoOfflineAsync(Guid courierId, CancellationToken cancellationToken = default)
    {
        var state = await courierStateRepository.GetByIdAsync(courierId, trackChanges: true, cancellationToken)
                    ?? throw new CourierStateNotFoundException(courierId);

        if (state.Status == CourierStatus.Delivering)
        {
            throw new CourierBusyException(courierId);
        }

        state.Status = CourierStatus.Offline;

        await unitOfWork.SaveChangesAsync(cancellationToken);
        await courierLocationStore.SetOfflineAsync(courierId, cancellationToken);
    }

    public async Task<bool> UpdateLocationAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken = default)
    {
        if (IsStale(location))
        {
            return false;
        }

        var result = await courierLocationStore.UpdateLocationAsync(courierId, location, cancellationToken);

        return result switch
        {
            LocationUpdateResult.Accepted => true,
            LocationUpdateResult.CourierNotTracked => await RestoreTrackingAsync(courierId, location, cancellationToken),
            _ => false
        };
    }

    private async Task<bool> RestoreTrackingAsync(Guid courierId, CourierLocation location, CancellationToken cancellationToken)
    {
        var state = await courierStateRepository.GetByIdAsync(courierId, cancellationToken: cancellationToken);

        switch (state?.Status)
        {
            case CourierStatus.Available:
                await courierLocationStore.SetAvailableAsync(courierId, location, cancellationToken);
                return true;
            case CourierStatus.Delivering:
                await courierLocationStore.SetDeliveringAsync(courierId, location, cancellationToken);
                return true;
            default:
                return false;
        }
    }

    private bool IsStale(CourierLocation location)
    {
        return timeProvider.GetUtcNow() - location.Timestamp > MaxLocationAge;
    }
}
