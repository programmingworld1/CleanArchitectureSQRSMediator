using Application.Mediator.Artist.Models;
using Application.Mediator.LibraryImporter.Models;
using Mapster;

namespace Application.Mappings
{
    public class ArtistApplicationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Artist, Mediator.Artist.Models.FindArtistResult>();

            config.NewConfig<CreateArtistItem, Domain.Entities.Artist>()
                .MapToConstructor(true);

            config.NewConfig<ArtistJsonDto, Domain.Entities.Artist>()
                .Ignore(x => x.Id);
        }
    }
}
