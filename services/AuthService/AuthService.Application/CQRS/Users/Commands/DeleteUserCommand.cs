using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthService.Application.CQRS.Users.Commands
{
    public record DeleteUserCommand(Guid Id) : IRequest<IdentityResult>;
}
