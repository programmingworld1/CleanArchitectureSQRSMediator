using Application.Interfaces;
using Application.Mediatr.Authentication.Models;
using Application.Persistance;

namespace Application.Services.Queries
{
    public class AuthenticationQueryService : IAuthenticationQueryService
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationQueryService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                throw new Exception("User does not exists");
            }

            if(user.Password != password)
            {
                throw new Exception("Password incorrect");
            }

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthenticationResult()
            {
                User = user,
                Token = token
            };
        }
    }
}
