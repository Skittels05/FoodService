using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers
{
    public class GetPendingCouriersHandler : IRequestHandler<GetPendingCouriersQuery, IEnumerable<CourierDto>>
    {
        private readonly IGenericRepository<Courier> _courierRepository;
        private readonly IMapper _mapper;

        public GetPendingCouriersHandler(IGenericRepository<Courier> courierRepository, IMapper mapper)
        {
            _courierRepository = courierRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CourierDto>> Handle(GetPendingCouriersQuery request, CancellationToken cancellationToken)
        {
            var couriers = await _courierRepository.FindAsync(c => c.IsVerified == false);
            return _mapper.Map<IEnumerable<CourierDto>>(couriers);
        }
    }
}
