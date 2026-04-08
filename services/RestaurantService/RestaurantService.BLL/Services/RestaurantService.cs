using RestaurantService.BLL.Common;
using RestaurantService.BLL.DTOs;
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
    public async Task<PagedList<RestaurantDto>> GetAllAsync(PageRequest request, CancellationToken cancellationToken = default)
    {
        var pagedRestaurants = await restaurantRepository.GetAllAsync(request, cancellationToken);
        return mappingService.MapPagedList<Restaurant, RestaurantDto>(pagedRestaurants);
    }

    public async Task<RestaurantDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var restaurant = await restaurantRepository.GetByIdWithDocumentsAsync(id, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), id);

        return mappingService.Map<Restaurant, RestaurantDto>(restaurant);
    }

    public async Task<Guid> CreateAsync(CreateRestaurantDto dto, CancellationToken cancellationToken = default)
    {
        var restaurant = mappingService.Map<CreateRestaurantDto, Restaurant>(dto);

        await restaurantRepository.AddAsync(restaurant, cancellationToken);
        return restaurant.Id;
    }

    public async Task UpdateAsync(UpdateRestaurantDto dto, CancellationToken cancellationToken = default)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(dto.Id, cancellationToken, true)
            ?? throw new NotFoundException(nameof(Restaurant), dto.Id);

        restaurant.Name = dto.Name;
        await restaurantRepository.UpdateAsync(restaurant, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var isDeleted = await restaurantRepository.DeleteAsync(id, cancellationToken);
        if (!isDeleted)
            throw new NotFoundException(nameof(Restaurant), id);
    }

    public async Task UpdateActiveStatusAsync(UpdateRestaurantStatusDto dto, CancellationToken cancellationToken = default)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(dto.Id, cancellationToken, true)
            ?? throw new NotFoundException(nameof(Restaurant), dto.Id);

        if (restaurant.IsActive == dto.IsActive) return;

        if (dto.IsActive && !restaurant.IsVerified)
            throw new RestaurantNotVerifiedException(dto.Id);

        restaurant.IsActive = dto.IsActive;
        await restaurantRepository.UpdateAsync(restaurant, cancellationToken);
    }

    public async Task VerifyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var restaurant = await restaurantRepository.GetByIdWithDocumentsAsync(id, cancellationToken, true)
            ?? throw new NotFoundException(nameof(Restaurant), id);

        if (restaurant.Documents is [])
            throw new MissingRestaurantDocumentsException(id);

        if (restaurant.Documents.Any(d => d.Status is not VerificationStatus.Approved))
            throw new UnapprovedDocumentsException(id);

        restaurant.IsVerified = true;
        restaurant.IsActive = true;

        await restaurantRepository.UpdateAsync(restaurant, cancellationToken);
    }
}
