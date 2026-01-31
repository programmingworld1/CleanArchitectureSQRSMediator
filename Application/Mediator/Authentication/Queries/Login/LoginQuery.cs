using Application.Mediator.Authentication.Models;
using MediatR;

namespace Application.Mediator.Authentication.Queries.Login
{
    public record LoginQuery(
        string Email,
        string Password
    ) : IRequest<AuthenticationResult>;
}
