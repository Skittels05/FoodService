using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class UpdateRestaurantManagerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateRestaurantManagerCommand>
{
    public async Task Handle(UpdateRestaurantManagerCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var manager = await unitOfWork.RestaurantManagerRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(RestaurantManager), request.Id);
            manager.ChangeName(request.Name);
            manager.ChangeRestaurantId(request.ManagedRestaurantId);
            await unitOfWork.RestaurantManagerRepository.UpdateAsync(manager, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
