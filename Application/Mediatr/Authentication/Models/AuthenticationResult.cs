using Domain.Entities;

namespace Application.Mediatr.Authentication.Models
{
    public class AuthenticationResult
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
