using Application.Mediator.Artist.Models;
using Application.Mediator.LibraryImporter.Models;
using Mapster;

namespace Application.Mappings
{
    public class ArtistApplicationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Domain.Entities.Artist, FindArtistResult>();

            config.NewConfig<CreateArtistItem, Domain.Entities.Artist>()
                .MapToConstructor(true);

            config.NewConfig<ArtistJsonDto, Domain.Entities.Artist>()
                .MapToConstructor(true)
                .Ignore(x => x.Id);
        }
    }
}
