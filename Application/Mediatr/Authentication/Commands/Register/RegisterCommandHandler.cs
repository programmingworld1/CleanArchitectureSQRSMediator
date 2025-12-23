using Application.Errors;
using Application.Interfaces;
using Application.Mediatr.Authentication.Models;
using Application.Persistance;
using Domain.Entities;
using MediatR;

namespace Application.Mediatr.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(command.Email) != null)
            {
                throw new DuplicateEmailException("Email does already exists.");
            }

            var user = new User()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
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
