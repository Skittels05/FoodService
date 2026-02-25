using AuthService.Application.CQRS.Users.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using MediatR;

public class SyncAuth0UserCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<SyncAuth0UserCommand, Guid>
{
    public async Task<Guid> Handle(SyncAuth0UserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await unitOfWork.UserRepository
            .GetByAuth0IdAsync(request.Auth0Id, cancellationToken);
        if (existingUser is not null)
        {
            return existingUser.Id;
        }
        var user = new User(request.Auth0Id, request.Email, request.UserName, UserRole.None);
        await unitOfWork.UserRepository.AddAsync(user, cancellationToken);
        return user.Id;
    }
}
