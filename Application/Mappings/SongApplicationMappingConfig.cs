using Application.Mediator.LibraryImporter.Models;
using Application.Mediator.Song.Commands;
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
                .Map(x => x.Bpm, y => y.Bpm ?? 0)
                .Ignore(x => x.Artist)
                .Ignore(x => x.Id)
                .MapToConstructor(true);

            config.NewConfig<Song, SongDto>()
                 .Map(dest => dest.ArtistName, src => src.Artist != null ? src.Artist.Name : null);

            config.NewConfig<CreateSongCommand, Song>()
                .MapToConstructor(true);
        }
    }
}
