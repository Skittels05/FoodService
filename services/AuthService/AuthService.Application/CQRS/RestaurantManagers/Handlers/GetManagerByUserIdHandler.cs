using AuthService.Application.CQRS.RestaurantManager.Queries;
using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Application.Exceptions;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManager.Handlers;

public class GetManagerByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetManagerByUserIdQuery, RestaurantManagerDto?>
{
    public async Task<RestaurantManagerDto?> Handle(GetManagerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var manager = await unitOfWork.RestaurantManagerRepository.GetByUserIdAsync(request.UserId, false, cancellationToken)
            ?? throw new NotFoundByUserException(nameof(RestaurantManager), request.UserId);
        return mapper.Map<RestaurantManagerDto>(manager);
    }
}
