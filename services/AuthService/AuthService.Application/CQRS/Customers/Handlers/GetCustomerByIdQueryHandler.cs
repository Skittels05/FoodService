using AuthService.Application.CQRS.Customers.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

public class GetCustomerByIdQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.CustomerRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(Customer), request.Id);

        var customerUser = await unitOfWork.UserRepository.GetByIdAsync(customer.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), customer.UserId);

        if (currentUserService.Role != "Admin" && currentUserService.Auth0Id != customerUser.Auth0Id)
            throw new AccessDeniedException();

        return mapper.Map<CustomerDto>(customer);
    }
}
