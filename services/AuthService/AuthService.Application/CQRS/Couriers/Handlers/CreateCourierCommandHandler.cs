using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers
{
    public class CreateCourierCommandHandler : IRequestHandler<CreateCourierCommand, Guid>
    {
        private readonly IGenericRepository<Courier> _courierRepository;

        public CreateCourierCommandHandler(IGenericRepository<Courier> courierRepository)
        {
            _courierRepository = courierRepository;
        }

        public async Task<Guid> Handle(CreateCourierCommand request, CancellationToken cancelationToken)
        {
            var courier = new Courier(
                request.UserId,
                request.Name,
                request.VehicleType,
                request.DocumentsPath,
                request.PhotoVerificationPath);

            await _courierRepository.AddAsync(courier);
            return courier.Id;
        }
    }
}
