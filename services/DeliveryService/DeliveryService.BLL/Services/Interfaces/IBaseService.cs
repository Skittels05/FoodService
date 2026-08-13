namespace DeliveryService.BLL.Services.Interfaces;

public interface IBaseService<TDto>
{
    Task<TDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
