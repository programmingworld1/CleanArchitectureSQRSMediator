using Application.Errors;
using Application.Interfaces;
using Application.Mediatr.Authentication.Models;
using Application.Persistance;
using Domain.Entities;

namespace Application.Services.Commands
{
    public class AuthenticationCommandService : IAuthenticationCommandService
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationCommandService(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            if (_userRepository.GetUserByEmail(email) != null)
            {
                throw new DuplicateEmailException("Email does already exists.");
            }

            var user = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);

            var token = _tokenGenerator.GenerateToken(user);

            return new AuthenticationResult()
            { 
                User = user,
                Token = token
            };
        }
    }
}
