using AuthService.Application.CQRS.RestaurantManager.Queries;
using AuthService.Application.DTO.RestaurantManager;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManager.Handlers;

public class GetManagerByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetManagerByUserIdQuery, RestaurantManagerDto?>
{
    public async Task<RestaurantManagerDto?> Handle(GetManagerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var manager = await unitOfWork.RestaurantManagerRepository.GetByUserIdAsync(request.UserId, cancellationToken);
        return manager is null ? null : mapper.Map<RestaurantManagerDto>(manager);
    }
}
