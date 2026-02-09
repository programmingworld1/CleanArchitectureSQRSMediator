using Application.Mediator.LibraryImporter.Models;
using Application.Mediator.Song.Commands;
using Application.Mediator.Song.Models;
using Domain.Entities;
using Domain.ValueObjects;
using Mapster;

namespace Application.Mappings
{
    public class SongApplicationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateSongCommand, Song>()
                .MapWith(src => new Song(
                    src.Name,
                    new Year(src.Year),
                    new Bpm(src.Bpm),
                    new Duration(src.Duration),
                    src.Shortname,
                    src.Genre,
                    src.SpotifyId,
                    src.Album
                ));

            config.NewConfig<SongJsonDto, Song>()
                .MapWith(src => new Song(
                    src.Name,
                    new Year(src.Year),
                    new Bpm(src.Bpm.Value),
                    new Duration(src.Duration),
                    src.Shortname,
                    src.Genre,
                    src.SpotifyId,
                    src.Album
                ));

            config.NewConfig<Song, SongDto>()
                .Map(dest => dest.Bpm, src => src.Bpm.Value)
                .Map(dest => dest.Duration, src => src.Duration.TotalSeconds)
                .Map(dest => dest.Year, src => src.Year.Value)
                .Map(dest => dest.ArtistName, src => src.Artist != null ? src.Artist.Name : null);
        }
    }
}
