using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class GetCourierByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetCourierByUserIdQuery, CourierDto?>
{
    public async Task<CourierDto?> Handle(GetCourierByUserIdQuery request, CancellationToken cancellationToken)
    {
        var courier = await unitOfWork.CourierRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundByUserException(nameof(Courier), request.UserId);
        return mapper.Map<CourierDto>(courier);
    }
}
