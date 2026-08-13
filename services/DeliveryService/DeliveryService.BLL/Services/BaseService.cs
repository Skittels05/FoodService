namespace DeliveryService.BLL.Services;

using DeliveryService.BLL.Exceptions;
using DeliveryService.BLL.Mappers.Interfaces;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.BLL.Services.Interfaces;
using Wolverine;

public abstract class BaseService<TEntity, TDto>(
    IGenericRepository<TEntity> repository,
    IMappingService mappingService,
    IUnitOfWork unitOfWork,
    IMessageBus bus) : IBaseService<TDto> where TEntity : BaseModel
{
    public virtual async Task<TDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, trackChanges: false, cancellationToken);
        return entity is null ? default : mappingService.Map<TEntity, TDto>(entity);
    }

    protected async Task<TEntity> GetEntityOrThrowAsync(Guid id, bool trackChanges, CancellationToken cancellationToken = default)
    {
        return await repository.GetByIdAsync(id, trackChanges, cancellationToken)
               ?? throw new NotFoundException(typeof(TEntity).Name, id);
    }

    protected async Task SaveAndPublishAsync<TEvent>(
        TEvent @event, 
        CancellationToken cancellationToken = default) where TEvent : class
    {
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await bus.PublishAsync(@event);
    }
}
