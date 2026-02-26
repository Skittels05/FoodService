using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuthService.Application.CQRS.Customers.Handlers;

public class CreateCustomerCommandHandler(
    IUnitOfWork unitOfWork,
    IAuth0RoleService auth0RoleService,
    ICurrentUserService currentUserService,
    IMapper mapper)
    : IRequestHandler<CreateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var auth0Id = currentUserService.Auth0Id
            ?? throw new UnauthorizedException();
        var user = await unitOfWork.UserRepository.GetByAuth0IdAsync(auth0Id, cancellationToken)
            ?? throw new NotFoundByAuth0Exception(auth0Id);
        if (user.Role is not UserRole.None)
            throw new RoleAlreadyAssignedException();
        request.UserId = user.Id;
        var customer = mapper.Map<Customer>(request);
        await unitOfWork.CustomerRepository.AddAsync(customer, cancellationToken);
        user.AssignRole(UserRole.Customer);
        await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        await auth0RoleService.AssignCustomerRoleAsync(user.Auth0Id, cancellationToken);
        return customer.Id;
    }
}
