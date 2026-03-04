using AuthService.Application.CQRS.Couriers.Queries;
using AuthService.Application.DTO.Courier;
using AuthService.Application.Exceptions;
using AuthService.Application.Extensions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Couriers.Handlers;

public class GetCourierByUserIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetCourierByUserIdQuery, CourierDto?>
{
    public async Task<CourierDto?> Handle(GetCourierByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.UserId);

        currentUserService.EnsureHasAccessToResource(user.Auth0Id);

        var courier = await unitOfWork.CourierRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(Courier), request.UserId);

        return mapper.Map<CourierDto>(courier);
    }
}
