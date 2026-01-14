using Domain.Entities;

namespace Application.InfraInterfaces.Persistance
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        void Add(User user);
    }
}
