using Application.Mediator.Authentication.Commands.Register;
using Application.Mediator.Authentication.Models;
using Application.Mediator.Authentication.Queries.Login;
using Contracts.Authentication;
using Mapster;

namespace Rockstar.Mappings
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.User);
        }
    }
}
