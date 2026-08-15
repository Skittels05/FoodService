namespace DeliveryService.BLL.Services;

using DeliveryService.BLL.Exceptions;
using DeliveryService.BLL.Mappers.Interfaces;
using DeliveryService.BLL.Models;
using DeliveryService.BLL.Repositories.Interfaces;
using DeliveryService.BLL.Services.Interfaces;

public abstract class BaseService<TEntity, TDto>(
    IGenericRepository<TEntity> repository,
    IMappingService mappingService,
    IUnitOfWork unitOfWork,
    IOutboxWriter outboxWriter) : IBaseService<TDto> where TEntity : BaseModel
{
    protected readonly IGenericRepository<TEntity> Repository = repository;
    protected readonly IMappingService MappingService = mappingService;
    protected readonly IUnitOfWork UnitOfWork = unitOfWork;
    protected readonly IOutboxWriter OutboxWriter = outboxWriter;

    public virtual async Task<TDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, trackChanges: false, cancellationToken);
        return entity is null ? default : MappingService.Map<TEntity, TDto>(entity);
    }

    protected async Task<TEntity> GetEntityOrThrowAsync(Guid id, bool trackChanges, CancellationToken cancellationToken = default)
    {
        return await Repository.GetByIdAsync(id, trackChanges, cancellationToken)
               ?? throw new NotFoundException(typeof(TEntity).Name, id);
    }

    protected async Task SaveAndPublishAsync<TEvent>(
        TEvent @event, 
        CancellationToken cancellationToken = default) where TEvent : class
    {
        OutboxWriter.Write(@event);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
