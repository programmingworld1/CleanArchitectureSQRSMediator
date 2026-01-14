using Application.InfraInterfaces;
using Application.InfraInterfaces.Persistance;
using Application.Mediator.LibraryImporter.Models;
using MapsterMapper;
using MediatR;
using System.Text.Json;

namespace Application.Mediator.LibraryImporter.Commands
{
    public class ImportLibraryCommandHandler : IRequestHandler<ImportLibraryCommand>
    {
        private readonly IMapper _mapper;
        private readonly IArtistRepository _artistRepository;
        private readonly IGitHubClient _gitHubClient;

        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        public ImportLibraryCommandHandler(
            IMapper mapper,
            IArtistRepository artistRepository,
            IGitHubClient gitHubClient)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
            _gitHubClient = gitHubClient;
        }

        public async Task Handle(ImportLibraryCommand command, CancellationToken cancellationToken)
        {
            var artistDtos = await _gitHubClient.DownloadArtistsAsync();
            var songDtos = await _gitHubClient.DownloadSongsAsync();

            // haal alle artists + songs op
            var artists = await _artistRepository.GetAllArtistsIncludingSongs();

            // 1) zorg dat alle artists bestaan
            foreach (var artistDto in artistDtos)
            {
                if (string.IsNullOrWhiteSpace(artistDto.Name))
                    continue;

                var artist = artists.FirstOrDefault(a => a.Name == artistDto.Name);

                if (artist == null)
                {
                    artist = _mapper.Map<Domain.Entities.Artist>(artistDto);
                    artists.Add(artist);
                    await _artistRepository.Add(artist);
                }
            }

            // 2) voeg songs toe (als ze nog niet bestaan)
            foreach (var songDto in songDtos)
            {
                if (string.IsNullOrWhiteSpace(songDto.Name))
                    continue;

                if (string.IsNullOrWhiteSpace(songDto.Artist))
                    continue;

                var artist = artists.FirstOrDefault(a => a.Name == songDto.Artist);

                if (artist == null)
                    continue;

                if (artist.Songs.Any(s => s.Name == songDto.Name))
                    continue;

                var song = _mapper.Map<Domain.Entities.Song>(songDto);
                artist.Songs.Add(song);
            }

            // 3) alles in één keer opslaan
            await _artistRepository.CommitAsync();
        }
    }
}
