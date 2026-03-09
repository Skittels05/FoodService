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
    public async Task<Guid> AddDocumentAsync(Guid restaurantId, AddRestaurantDocumentDto dto, CancellationToken ct = default)
    {
        var restaurant = await restaurantRepository.GetByIdAsync(restaurantId, ct)
            ?? throw new NotFoundException(nameof(Restaurant), restaurantId);

        if (restaurant.IsVerified is true)
            throw new RestaurantAlreadyVerifiedException(restaurantId);

        var document = dto.ToEntity(restaurantId);

        await documentRepository.AddAsync(document, ct);
        return document.Id;
    }

    public async Task RemoveDocumentAsync(Guid documentId, CancellationToken ct = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, ct)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        var restaurant = await restaurantRepository.GetByIdAsync(document.RestaurantId, ct);

        if (restaurant is { IsVerified: true })
            throw new RestaurantAlreadyVerifiedException(restaurant.Id);

        await documentRepository.DeleteAsync(documentId, ct);
    }

    public async Task ReplaceDocumentAsync(Guid documentId, ReplaceRestaurantDocumentDto dto, CancellationToken ct = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, ct)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        var restaurant = await restaurantRepository.GetByIdAsync(document.RestaurantId, ct);

        if (restaurant is { IsVerified: true })
            throw new RestaurantAlreadyVerifiedException(restaurant.Id);

        document.FileUrl = dto.NewFileUrl;
        document.Status = VerificationStatus.Pending;
        document.RejectionReason = null;

        await documentRepository.UpdateAsync(document, ct);
    }

    public async Task ApproveDocumentAsync(Guid documentId, CancellationToken ct = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, ct)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        document.Status = VerificationStatus.Approved;
        document.RejectionReason = null;

        await documentRepository.UpdateAsync(document, ct);
    }

    public async Task RejectDocumentAsync(Guid documentId, RejectRestaurantDocumentDto dto, CancellationToken ct = default)
    {
        var document = await documentRepository.GetByIdAsync(documentId, ct)
            ?? throw new NotFoundException(nameof(RestaurantDocument), documentId);

        document.Status = VerificationStatus.Rejected;
        document.RejectionReason = dto.Reason;

        await documentRepository.UpdateAsync(document, ct);
    }
}
