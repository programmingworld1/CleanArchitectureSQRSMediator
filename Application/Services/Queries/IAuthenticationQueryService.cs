using Application.Mediatr.Authentication.Models;

namespace Application.Services.Queries
{
    public interface IAuthenticationQueryService
    {
        AuthenticationResult Login(string email, string password);
    }
}
