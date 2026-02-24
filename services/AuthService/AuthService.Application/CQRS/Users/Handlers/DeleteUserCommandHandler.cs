using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var isDeleted = await unitOfWork.UserRepository.DeleteAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }
    }
}
