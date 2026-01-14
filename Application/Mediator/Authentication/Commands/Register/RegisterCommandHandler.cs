using Application.Errors;
using Application.InfraInterfaces;
using Application.InfraInterfaces.Persistance;
using Application.Mediator.Authentication.Models;
using Domain.Entities;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(
            IMapper mapper,
            IJwtTokenGenerator tokenGenerator, 
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(command.Email) != null)
            {
                throw new DuplicateEmailException("Email does already exists.");
            }

            if (!command.Email.EndsWith("@teamrockstars.nl", StringComparison.OrdinalIgnoreCase))
            {
                throw new WrongEmailException("Invalid e-mail. Only rockstarts can register.");
            }

            var user = _mapper.Map<User>(command);

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
