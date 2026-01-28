using AuthService.Application.CQRS.RestaurantManagers.Queries;
using AuthService.Application.DTO.RestaurantManagers;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManager.Handlers;

public class GetRestaurantManagerByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetRestaurantManagerByIdQuery, RestaurantManagerDto?>
{
    public async Task<RestaurantManagerDto?> Handle(GetRestaurantManagerByIdQuery request, CancellationToken cancellationToken)
    {
        var manager = await unitOfWork.RestaurantManagerRepository.GetByIdAsync(request.Id, cancellationToken);
        return manager is null ? null : mapper.Map<RestaurantManagerDto>(manager);
    }
}
