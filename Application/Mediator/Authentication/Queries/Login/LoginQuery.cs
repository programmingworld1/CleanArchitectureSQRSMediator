using Application.Mediator.Authentication.Models;
using MediatR;

namespace Application.Mediator.Authentication.Queries.Login
{
    public class LoginQuery : IRequest<AuthenticationResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
