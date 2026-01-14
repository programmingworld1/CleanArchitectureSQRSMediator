using Domain.Entities;

namespace Application.Mediator.Authentication.Models
{
    public class AuthenticationResult
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
