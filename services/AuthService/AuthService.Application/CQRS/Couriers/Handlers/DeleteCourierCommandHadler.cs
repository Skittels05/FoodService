using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers
{
    public class DeleteCourierCommandHadler : IRequestHandler<DeleteCourierCommand>
    {
        private readonly IGenericRepository<Courier> _courierRepository;

        public DeleteCourierCommandHadler(IGenericRepository<Courier> courierRepository)
        {
            _courierRepository = courierRepository;
        }

        public async Task Handle(DeleteCourierCommand request, CancellationToken cancellationToken)
        {
            await _courierRepository.DeleteAsync(request.Id);
        }
    }
}
