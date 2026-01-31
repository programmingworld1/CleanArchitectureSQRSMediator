using Application.Mediator.Authentication.Commands.Create;
using Mapster;

namespace Application.Mappings
{
    public class AuthenticationApplicationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateUserCommand, Domain.Entities.User>()
                .MapToConstructor(true); // because the empty ctor is private, MapToConstructor uses the public ctor and it will mapp the values from the command with the values with user only if they match name and type
        }
    }
}
