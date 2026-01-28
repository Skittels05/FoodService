using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken)
                  ?? throw new NotFoundException(nameof(User), request.Id);
            var result = await unitOfWork.UserRepository.DeleteAsync(user, cancellationToken);
            if (!result.Succeeded)
            {
                var errorMsg = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to delete user: {errorMsg}");
            }
            await unitOfWork.CommitTransactionAsync(cancellationToken);
            return result;
        }
        catch (Exception)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
