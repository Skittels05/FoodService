using AutoMapper;
using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(request.Id.ToString())
                ?? throw new NotFoundException(nameof(User), request.Id);
            mapper.Map(request, user);
            var result = await unitOfWork.UserManager.UpdateAsync(user);
            if (result is { Succeeded: false })
            {
                var errorMsg = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to update user: {errorMsg}");
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
