using Application.Mediatr.Authentication.Models;
using MediatR;

namespace Application.Mediatr.Authentication.Queries.Login
{
    public class LoginQuery : IRequest<AuthenticationResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
