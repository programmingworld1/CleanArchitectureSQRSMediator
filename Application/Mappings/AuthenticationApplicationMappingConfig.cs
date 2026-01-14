using Application.Mediator.Artist.Models;
using Application.Mediator.Authentication.Commands.Register;
using Application.Mediator.LibraryImporter.Models;
using Mapster;

namespace Application.Mappings
{
    public class AuthenticationApplicationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterCommand, Domain.Entities.User>();
            //config.NewConfig<ArtistJsonDto, Domain.Entities.Artist>()
            //    .Ignore(x => x.Id);
        }
    }
}
