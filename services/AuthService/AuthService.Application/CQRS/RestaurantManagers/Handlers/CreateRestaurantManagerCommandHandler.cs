using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class CreateRestaurantManagerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateRestaurantManagerCommand, Guid>
{
    public async Task<Guid> Handle(CreateRestaurantManagerCommand request, CancellationToken cancellationToken)
    {
        var manager = mapper.Map<Domain.Entities.RestaurantManager>(request);
        await unitOfWork.RestaurantManagerRepository.AddAsync(manager, cancellationToken);
        return manager.Id;
    }
}
