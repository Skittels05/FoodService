using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class DeleteCourierCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCourierCommand>
{
    public async Task Handle(DeleteCourierCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await unitOfWork.CourierRepository.DeleteAsync(request.Id, cancellationToken);
        if (isDeleted is false)
        {
            throw new NotFoundException(nameof(Courier), request.Id);
        }
    }
}
