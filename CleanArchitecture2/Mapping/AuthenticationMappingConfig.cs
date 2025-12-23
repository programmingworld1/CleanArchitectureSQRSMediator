using Application.Mediatr.Authentication.Commands.Register;
using Application.Mediatr.Authentication.Models;
using Application.Mediatr.Authentication.Queries.Login;
using Contracts.Authentication;
using Mapster;

namespace CleanArchitecture2.Mapping
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
