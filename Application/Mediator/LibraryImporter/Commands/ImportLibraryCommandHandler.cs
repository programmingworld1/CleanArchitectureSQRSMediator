using Application.InfraInterfaces;
using Application.InfraInterfaces.Persistance;
using Application.Mediator.LibraryImporter.Models;
using FluentValidation;
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
        private readonly IValidator<SongJsonDto> _songValidator;

        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        public ImportLibraryCommandHandler(
            IMapper mapper,
            IArtistRepository artistRepository,
            IGitHubClient gitHubClient,
            IValidator<SongJsonDto> songValidator)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
            _gitHubClient = gitHubClient;
            _songValidator = songValidator;
        }

        public async Task Handle(ImportLibraryCommand command, CancellationToken cancellationToken)
        {
            var artistDtos = await _gitHubClient.DownloadArtistsAsync();
            var songDtos = await _gitHubClient.DownloadSongsAsync();

            var artists = await _artistRepository.GetAllArtistsIncludingSongs();

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

            foreach (var songDto in songDtos)
            {
                var validation = await _songValidator.ValidateAsync(songDto, cancellationToken);

                if (!validation.IsValid)
                {
                    continue; // skip this record
                }

                if (songDto.Year >= 2016)
                    continue;

                if (string.IsNullOrWhiteSpace(songDto.Genre) ||
                    !songDto.Genre.Contains("Metal", StringComparison.OrdinalIgnoreCase))
                    continue;

                var artist = artists.FirstOrDefault(a => a.Name == songDto.Artist);
                if (artist == null)
                    continue;

                if (artist.HasSong(songDto.Name))
                    continue;

                var song = _mapper.Map<Domain.Entities.Song>(songDto);

                artist.AddSong(song);
            }

            await _artistRepository.CommitAsync();
        }
    }
}
