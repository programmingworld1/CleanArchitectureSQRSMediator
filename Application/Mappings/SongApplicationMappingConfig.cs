using Application.Mediator.LibraryImporter.Models;
using Application.Mediator.Song.Models;
using Domain.Entities;
using Mapster;

namespace Application.Mappings
{
    public class SongApplicationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<SongJsonDto, Song>()
                .Map(x => x.ArtistName, y => y.Artist)
                .Map(x => x.Bpm, y => y.Bpm ?? 0)
                .Ignore(x => x.Artist)
                .Ignore(x => x.Id)
                .Ignore(x => x.ArtistId);

            config.NewConfig<Song, SongDto>();
        }
    }
}
