using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class DeleteCourierCommandHadler(IGenericRepository<Courier> courierRepository)
    : IRequestHandler<DeleteCourierCommand>
{
    public async Task Handle(DeleteCourierCommand request, CancellationToken cancellationToken)
    {
        await courierRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
