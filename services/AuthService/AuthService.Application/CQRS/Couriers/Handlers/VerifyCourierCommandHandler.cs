using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers
{
    public class VerifyCourierCommandHandler: IRequestHandler<VerifyCourierCommand>
    {
        private IGenericRepository<Courier> _courierRepository;
        public VerifyCourierCommandHandler(IGenericRepository<Courier> courierRepository) { 
            _courierRepository= courierRepository;
        }
        public async Task Handle(VerifyCourierCommand request, CancellationToken cancelationToken)
        {
            var courier = await _courierRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Courier), request.Id);
            courier.Verify();
            await _courierRepository.UpdateAsync(courier);
        }
    }
}
