using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class DeleteCourierCommandHandler(ICourierRepository courierRepository)
    : IRequestHandler<DeleteCourierCommand>
{
    public async Task Handle(DeleteCourierCommand request, CancellationToken cancellationToken)
    {
        await courierRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
