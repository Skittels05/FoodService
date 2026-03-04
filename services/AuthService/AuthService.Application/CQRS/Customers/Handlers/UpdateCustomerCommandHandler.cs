using AuthService.Application.CQRS.Customers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Application.Extensions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using MediatR;

public class UpdateCustomerCommandHandler(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService)
    : IRequestHandler<UpdateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Customer), request.Id);

        var customerUser = await unitOfWork.UserRepository.GetByIdAsync(customer.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), customer.UserId);

        currentUserService.EnsureHasAccessToResource(customerUser.Auth0Id);

        customer.ChangeName(request.Name);
        var updatedCustomer = await unitOfWork.CustomerRepository.UpdateAsync(customer, cancellationToken);
        return updatedCustomer.Id;
    }
}
