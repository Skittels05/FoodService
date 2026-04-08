using RestaurantService.BLL.DTOs;

namespace RestaurantService.BLL.Services.Interfaces;

public interface IRestaurantDocumentService
{
    Task<Guid> AddDocumentAsync(AddRestaurantDocumentDto dto, CancellationToken cancellationToken = default);
    Task RemoveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default);
    Task ReplaceDocumentAsync(ReplaceRestaurantDocumentDto dto, CancellationToken cancellationToken = default);
    Task ApproveDocumentAsync(Guid documentId, CancellationToken cancellationToken = default);
    Task RejectDocumentAsync(RejectRestaurantDocumentDto dto, CancellationToken cancellationToken = default);
}
