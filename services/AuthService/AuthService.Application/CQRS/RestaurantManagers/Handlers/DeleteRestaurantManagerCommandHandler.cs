using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class DeleteRestaurantManagerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteRestaurantManagerCommand>
{
    public async Task Handle(DeleteRestaurantManagerCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await unitOfWork.RestaurantManagerRepository.DeleteAsync(request.Id, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
