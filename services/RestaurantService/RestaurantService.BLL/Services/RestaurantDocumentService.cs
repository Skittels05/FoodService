using RestaurantService.BLL.DTOs;
using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Exceptions;
using RestaurantService.BLL.Extensions;
using RestaurantService.BLL.Interfaces;
using RestaurantService.BLL.Mappers.Interfaces;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.BLL.Services;

public class RestaurantDocumentService(
    IGenericRepository<RestaurantDocument> documentRepository,
    IRestaurantRepository restaurantRepository,
    IMappingService mappingService,
    ICurrentUserService currentUserService) : IRestaurantDocumentService
{
    public async Task<Guid> AddDocumentAsync(AddRestaurantDocumentDto dto, CancellationToken cancellationToken = default)
    {
        currentUserService.EnsureHasAccessToRestaurant(dto.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(dto.RestaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), dto.RestaurantId);

        if (restaurant.IsVerified)
            throw new RestaurantAlreadyVerifiedException(dto.RestaurantId);

        var document = mappingService.Map<AddRestaurantDocumentDto, RestaurantDocument>(dto);
        document.RestaurantId = dto.RestaurantId;

        await documentRepository.AddAsync(document, cancellationToken);
        return document.Id;
    }

    public async Task RemoveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, cancellationToken)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        currentUserService.EnsureHasAccessToRestaurant(document.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(document.RestaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), document.RestaurantId);

        if (restaurant.IsVerified)
            throw new RestaurantAlreadyVerifiedException(restaurant.Id);

        await documentRepository.DeleteAsync(documentId, cancellationToken);
    }

    public async Task ReplaceDocumentAsync(ReplaceRestaurantDocumentDto dto, CancellationToken cancellationToken = default)
    {
        var document = await documentRepository.GetByIdAsync(dto.Id, cancellationToken, true)
            ?? throw new NotFoundException(nameof(RestaurantDocument), dto.Id);
        
        currentUserService.EnsureHasAccessToRestaurant(document.RestaurantId);

        var restaurant = await restaurantRepository.GetByIdAsync(document.RestaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), document.RestaurantId);

        if (restaurant.IsVerified)
            throw new RestaurantAlreadyVerifiedException(restaurant.Id);

        document.FileUrl = dto.NewFileUrl;
        document.Status = VerificationStatus.Pending;
        document.RejectionReason = null;

        await documentRepository.UpdateAsync(document, cancellationToken);
    }

    public async Task ApproveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, cancellationToken, true)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        if (document.Status == VerificationStatus.Approved) 
            return;

        var restaurant = await restaurantRepository.GetByIdAsync(document.RestaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), document.RestaurantId);

        if (restaurant.IsVerified)
            throw new RestaurantAlreadyVerifiedException(restaurant.Id);

        document.Status = VerificationStatus.Approved;
        document.RejectionReason = null;

        await documentRepository.UpdateAsync(document, cancellationToken);
    }

    public async Task RejectDocumentAsync(RejectRestaurantDocumentDto dto, CancellationToken cancellationToken = default)
    {
        var document = await documentRepository.GetByIdAsync(dto.Id, cancellationToken, true)
            ?? throw new NotFoundException(nameof(RestaurantDocument), dto.Id);

        if (document.Status == VerificationStatus.Rejected && document.RejectionReason == dto.Reason) 
            return;

        var restaurant = await restaurantRepository.GetByIdAsync(document.RestaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), document.RestaurantId);

        if (restaurant.IsVerified)
            throw new RestaurantAlreadyVerifiedException(restaurant.Id);

        document.Status = VerificationStatus.Rejected;
        document.RejectionReason = dto.Reason;

        await documentRepository.UpdateAsync(document, cancellationToken);
    }
}
