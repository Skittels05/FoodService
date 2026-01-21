using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Application.CQRS.Users.Commands
{
    public record DeleteUserCommand(Guid Id) : IRequest;
}
