using Domain.Entities;

namespace Application.Mediator.Authentication.Models
{
    public record AuthenticationResult(User User, string Token);
}
