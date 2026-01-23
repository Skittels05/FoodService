using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers
{
    public class GetCourierByUserIdHandler : IRequestHandler<GetCourierByUserIdQuery, CourierDto?>
    {
        private readonly IGenericRepository<Courier> _courierRepository;
        private readonly IMapper _mapper;

        public GetCourierByUserIdHandler(IGenericRepository<Courier> courierRepository, IMapper mapper)
        {
            _courierRepository = courierRepository;
            _mapper = mapper;
        }

        public async Task<CourierDto?> Handle(GetCourierByUserIdQuery request, CancellationToken cancellationToken)
        {
            var couriers = await _courierRepository.FindAsync(c => c.UserId == request.UserId);
            var courier = couriers.FirstOrDefault()
                ?? throw new NotFoundByUserException(nameof(Courier), request.UserId);

            return _mapper.Map<CourierDto>(courier);
        }
    }
}
