using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers
{
    public class GetCourierByIdHandler : IRequestHandler<GetCourierByIdQuery, CourierDto?>
    {
        private readonly IGenericRepository<Courier> _courierRepository;
        private readonly IMapper _mapper;

        public GetCourierByIdHandler(IGenericRepository<Courier> courierRepository, IMapper mapper)
        {
            _courierRepository = courierRepository;
            _mapper = mapper;
        }

        public async Task<CourierDto?> Handle(GetCourierByIdQuery request, CancellationToken cancellationToken)
        {
            var courier = await _courierRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Courier), request.Id);
            return _mapper.Map<CourierDto>(courier);
        }
    }
}
