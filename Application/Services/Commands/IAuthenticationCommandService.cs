using Application.Mediatr.Authentication.Models;

namespace Application.Services.Commands
{
    public interface IAuthenticationCommandService
    {
        AuthenticationResult Register(string firstName, string lastName, string username, string password);
    }
}
