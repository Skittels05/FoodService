using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Extensions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.CQRS.Users.Handlers;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateUserCommand, IdentityResult>
{
    public async Task<IdentityResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.Id);
        mapper.Map(request, user);
        var result = await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        result.EnsureSuccess();
        return result;
    }
}
