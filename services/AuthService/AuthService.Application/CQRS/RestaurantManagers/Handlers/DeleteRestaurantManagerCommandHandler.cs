using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Application.Exceptions;
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
            var manager = await unitOfWork.RestaurantManagerRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(RestaurantManager), request.Id);
            await unitOfWork.RestaurantManagerRepository.DeleteAsync(manager, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
