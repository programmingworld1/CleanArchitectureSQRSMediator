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
            config.NewConfig<SongRegisterRequest, SongRegisterCommand>();
            config.NewConfig<SongDeleteRequest, SongDeleteCommand>();
            config.NewConfig<SongsGetOnGenreRequest, SongsGetOnGenreRequest>();
        }
    }
}
