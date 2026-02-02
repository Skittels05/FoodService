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
        var isDeleted = await unitOfWork.RestaurantManagerRepository.DeleteAsync(request.Id, cancellationToken);
        if (isDeleted is false)
        {
            throw new NotFoundException(nameof(RestaurantManager), request.Id);
        }
    }
}
