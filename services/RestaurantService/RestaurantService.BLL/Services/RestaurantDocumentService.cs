using RestaurantService.BLL.DTOs.RestaurantDocument;
using RestaurantService.BLL.Enums;
using RestaurantService.BLL.Exceptions;
using RestaurantService.BLL.Mappers;
using RestaurantService.BLL.Models;
using RestaurantService.BLL.Repositories.Interfaces;
using RestaurantService.BLL.Services.Interfaces;

namespace RestaurantService.BLL.Services;

public class RestaurantDocumentService(
    IGenericRepository<RestaurantDocument> documentRepository,
    IRestaurantRepository restaurantRepository) : IRestaurantDocumentService
{
    public async Task<Guid> AddDocumentAsync(Guid restaurantId, AddRestaurantDocumentDto dto, CancellationToken cancellationToken = default)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(restaurantId, cancellationToken)
            ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        if (restaurant.IsVerified)
            throw new RestaurantAlreadyVerifiedException(restaurantId);

        var document = dto.ToEntity(restaurantId);
        await documentRepository.AddAsync(document, cancellationToken);
        return document.Id;
    }

    public async Task RemoveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, cancellationToken)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        var restaurant = await restaurantRepository.GetByIdAsync(document.RestaurantId, cancellationToken);

        if (restaurant != null && restaurant.IsVerified)
            throw new RestaurantAlreadyVerifiedException(restaurant.Id);

        await documentRepository.DeleteAsync(documentId, cancellationToken);
    }

    public async Task ReplaceDocumentAsync(Guid documentId, ReplaceRestaurantDocumentDto dto, CancellationToken cancellationToken = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, cancellationToken)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        var restaurant = await restaurantRepository.GetByIdAsync(document.RestaurantId, cancellationToken);

        if (restaurant != null && restaurant.IsVerified)
            throw new RestaurantAlreadyVerifiedException(restaurant.Id);

        document.FileUrl = dto.NewFileUrl;
        document.Status = VerificationStatus.Pending;
        document.RejectionReason = null;

        await documentRepository.UpdateAsync(document, cancellationToken);
    }

    public async Task ApproveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, cancellationToken)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        document.Status = VerificationStatus.Approved;
        document.RejectionReason = null;

        await documentRepository.UpdateAsync(document, cancellationToken);
    }

    public async Task RejectDocumentAsync(Guid documentId, RejectRestaurantDocumentDto dto, CancellationToken cancellationToken = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, cancellationToken)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        document.Status = VerificationStatus.Rejected;
        document.RejectionReason = dto.Reason;

        await documentRepository.UpdateAsync(document, cancellationToken);
    }
}
