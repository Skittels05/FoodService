using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.Id);

        mapper.Map(request, user);
        await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
    }
}
