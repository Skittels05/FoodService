using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Services.Interfaces;

public interface IRestaurantDocumentService
{
    Task<Guid> AddDocumentAsync(Guid restaurantId, AddRestaurantDocumentDto dto, CancellationToken cancellationToken = default);
    Task RemoveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default);
    Task ReplaceDocumentAsync(Guid documentId, ReplaceRestaurantDocumentDto dto, CancellationToken cancellationToken = default);
    Task ApproveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default);
    Task RejectDocumentAsync(Guid documentId, RejectRestaurantDocumentDto dto, CancellationToken cancellationToken = default);
}
