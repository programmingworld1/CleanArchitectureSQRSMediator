using Application.Mediator.Artist.Models;
using Application.Mediator.Song.Commands;
using Contracts.Song;
using Mapster;

namespace Rockstar.Mappings
{
    public class SongContractMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateSongRequest, CreateSongCommand>();
            config.NewConfig<DeleteSongRequest, DeleteSongCommand>();
            config.NewConfig<GetSongsByGenreRequest, GetSongsByGenreRequest>();
        }
    }
}
