using Application.Errors;
using Application.InfraInterfaces;
using Application.InfraInterfaces.Persistance;
using Application.Mediator.Authentication.Models;
using Application.Result;
using Domain.Entities;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Authentication.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<AuthenticationResult>>
    {
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(
            IMapper mapper,
            IJwtTokenGenerator tokenGenerator, 
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Result<AuthenticationResult>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(command.Email) != null)
            {
                return Result<AuthenticationResult>.Failure(
                    new Error(
                        "EmailAlreadyExists",
                        "This email address is already in use."
                    )
                );
            }

            if (!command.Email.EndsWith("@teamrockstars.nl", StringComparison.OrdinalIgnoreCase))
            {
                throw new WrongEmailException("Invalid e-mail. Only rockstarts can register.");
            }

            var user = _mapper.Map<User>(command);

            _userRepository.Add(user);

            var token = _tokenGenerator.GenerateToken(user);

            return Result<AuthenticationResult>.Success(new AuthenticationResult(user, token));
        }
    }
}
