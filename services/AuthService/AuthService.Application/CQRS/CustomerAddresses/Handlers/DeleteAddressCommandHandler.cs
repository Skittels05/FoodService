using AuthService.Application.CQRS.CustomerAddresses.Commands;
using AuthService.Application.Exceptions;
using AuthService.Domain.Interfaces;
using MediatR;

namespace AuthService.Application.CQRS.CustomerAddresses.Handlers
{
    public class DeleteAddressCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAddressCommand>
    {
        public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var isDeleted = await unitOfWork.CustomerAddressRepository.DeleteAsync(request.Id, cancellationToken);
            if (isDeleted is false)
            {
                throw new NotFoundException(nameof(CustomerAddresses), request.Id);
            }
        }
    }
}
