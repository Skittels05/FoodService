using AuthService.Application.CQRS.Customers.Queries;
using AuthService.Application.DTO.Customers;
using AuthService.Application.Exceptions;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

public class GetCustomerByUserIdQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICurrentUserService currentUserService)
    : IRequestHandler<GetCustomerByUserIdQuery, CustomerDto?>
{
    public async Task<CustomerDto?> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.UserId);

        if (currentUserService.Role != "Admin" && currentUserService.Auth0Id != user.Auth0Id)
            throw new AccessDeniedException();

        var customer = await unitOfWork.CustomerRepository.GetByUserIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundByUserException(nameof(Customer), request.UserId);

        return mapper.Map<CustomerDto>(customer);
    }
}
