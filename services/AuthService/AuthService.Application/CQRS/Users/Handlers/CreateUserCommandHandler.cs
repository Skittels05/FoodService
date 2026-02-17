using AuthService.Application.CQRS.Users.Commands;
using AuthService.Application.Extensions;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        var result = await unitOfWork.UserRepository.CreateAsync(user, request.Password, cancellationToken);
        result.EnsureSuccess();
        return user.Id;
    }
}
