using Application.Interfaces;
using Application.Mediatr.Authentication.Models;
using Application.Persistance;
using MediatR;

namespace Application.Mediatr.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        public LoginQueryHandler(IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserByEmail(request.Email);

            if (user == null)
            {
                throw new Exception("User does not exists");
            }

            if (user.Password != request.Password)
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
