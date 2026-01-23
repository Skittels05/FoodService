using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers
{
    public class UpdateCourierCommandHandler : IRequestHandler<UpdateCourierCommand>
    {
        private readonly IGenericRepository<Courier> _courierRepository;

        public UpdateCourierCommandHandler(IGenericRepository<Courier> courierRepository)
        {
            _courierRepository = courierRepository;
        }

        public async Task Handle(UpdateCourierCommand request, CancellationToken cancelationToken)
        {
            var courier = await _courierRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Courier), request.Id);
            courier.ChangeVehicle(request.VehicleType);
            courier.ChangeName(request.Name);

            await _courierRepository.UpdateAsync(courier);
        }
    }
}
