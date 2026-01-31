using Application.ApplicationResult;
using Application.Mediator.Authentication.Models;
using MediatR;

namespace Application.Mediator.Authentication.Commands.Create
{
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<Result<AuthenticationResult>>;
}
