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
            config.NewConfig<SongRegisterRequest, CreateSongCommand>();
            config.NewConfig<SongDeleteRequest, DeleteSongCommand>();
            config.NewConfig<SongsGetOnGenreRequest, SongsGetOnGenreRequest>();
        }
    }
}
