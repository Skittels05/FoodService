using AuthService.Application.CQRS.Couriers.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Entities;
using AuthService.Domain.Enums;
using AuthService.Domain.Interfaces;
using AutoMapper;
using MediatR;

public class CreateCourierCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateCourierCommand, Guid>
{
    public async Task<Guid> Handle(CreateCourierCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserId, cancellationToken)
            ?? throw new NotFoundException(nameof(User), request.UserId);
        if (user.Role is not UserRole.None)
            throw new InvalidOperationException("User already has an assigned role.");
        var courier = mapper.Map<Courier>(request);
        await unitOfWork.CourierRepository.AddAsync(courier, cancellationToken);
        user.AssignRole(UserRole.Courier);
        await unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);
        return courier.Id;
    }
}
