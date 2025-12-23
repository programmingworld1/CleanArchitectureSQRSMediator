using Application.Persistance;
using Domain.Entities;

namespace Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> _users = new List<User>();

        public void Add(User user)
        {
            _users.Add((user));
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }
    }
}
