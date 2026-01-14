using Domain.Entities;

namespace Application.InfraInterfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
