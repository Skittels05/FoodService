using AutoMapper;
using AuthService.Application.CQRS.Users.Commands;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.Users.Handlers;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request);
        var result = await unitOfWork.UserRepository.CreateAsync(user, request.Password, cancellationToken);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Failed to create user: {errors}");
        }
        return user.Id;
    }
}
