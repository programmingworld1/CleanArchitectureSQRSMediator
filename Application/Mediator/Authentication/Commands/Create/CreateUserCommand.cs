using Application.Mediator.Authentication.Models;
using Application.Result;
using MediatR;

namespace Application.Mediator.Authentication.Commands.Create
{
    public class CreateUserCommand : IRequest<Result<AuthenticationResult>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
