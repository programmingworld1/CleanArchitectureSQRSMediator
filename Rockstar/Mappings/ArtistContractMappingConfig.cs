using Application.Mediator.Artist.Commands;
using Application.Mediator.Artist.Models;
using Application.Mediator.Artist.Queries;
using Application.Mediator.Song.Commands;
using Contracts.Artist;
using Mapster;

namespace Rockstar.Mappings
{
    public class ArtistContractMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ArtistRegisterRequest, CreateArtistItem>();
            config.NewConfig<ArtistDeleteRequest, DeleteArtistCommand>();
            config.NewConfig<ArtistFindRequest, FindArtistQuery>();
        }
    }
}
