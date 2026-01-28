using AuthService.Application.CQRS.RestaurantManagers.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;

namespace AuthService.Application.CQRS.RestaurantManagers.Handlers;

public class CreateRestaurantManagerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateRestaurantManagerCommand, Guid>
{
    public async Task<Guid> Handle(CreateRestaurantManagerCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var manager = mapper.Map<RestaurantManager>(request);

            await unitOfWork.RestaurantManagerRepository.AddAsync(manager, cancellationToken);
            await unitOfWork.CommitTransactionAsync(cancellationToken);

            return manager.Id;
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}