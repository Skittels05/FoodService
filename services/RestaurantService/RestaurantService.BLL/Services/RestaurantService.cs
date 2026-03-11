using RestaurantService.BLL.DTOs.Restaurant;
using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Exceptions;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.BLL.Services;

public class RestaurantService(
    IRestaurantRepository restaurantRepository,
    IMappingService mappingService)
    : IRestaurantService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var restaurants = await restaurantRepository.GetAllAsync(cancellationToken);

        return restaurants.Select(mappingService.Map<Restaurant, RestaurantDto>);
    }

    public async Task<RestaurantDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var restaurant = await restaurantRepository.GetByIdWithDocumentsAsync(id, cancellationToken);

        return restaurant is not null
            ? mappingService.Map<Restaurant, RestaurantDto>(restaurant)
            : null;
    }

    public async Task<Guid> CreateAsync(CreateRestaurantDto dto, CancellationToken cancellationToken = default)
    {
        var restaurant = mappingService.Map<CreateRestaurantDto, Restaurant>(dto);

        await restaurantRepository.AddAsync(restaurant, cancellationToken);
        return restaurant.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateRestaurantDto dto, CancellationToken cancellationToken = default)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), id);

        restaurant.Name = dto.Name;
        await restaurantRepository.UpdateAsync(restaurant, cancellationToken);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        => restaurantRepository.DeleteAsync(id, cancellationToken);

    public async Task UpdateActiveStatusAsync(Guid restaurantId, bool isActive, CancellationToken cancellationToken = default)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(restaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        if (restaurant.IsActive == isActive) return;

        if (isActive && !restaurant.IsVerified)
            throw new RestaurantNotVerifiedException(restaurantId);

        restaurant.IsActive = isActive;
        await restaurantRepository.UpdateAsync(restaurant, cancellationToken);
    }

    public async Task VerifyAsync(Guid restaurantId, CancellationToken cancellationToken = default)
    {
        var restaurant = await restaurantRepository.GetByIdWithDocumentsAsync(restaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        if (restaurant.Documents is [])
            throw new MissingRestaurantDocumentsException(restaurantId);

        if (restaurant.Documents.Any(d => d.Status is not VerificationStatus.Approved))
            throw new UnapprovedDocumentsException(restaurantId);

        restaurant.IsVerified = true;
        restaurant.IsActive = true;

        await restaurantRepository.UpdateAsync(restaurant, cancellationToken);
    }
}
