using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class DeleteCourierCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCourierCommand>
{
    public async Task Handle(DeleteCourierCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            await unitOfWork.CourierRepository.DeleteAsync(request.Id, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
