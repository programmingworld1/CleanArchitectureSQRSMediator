using Application.InfraInterfaces.Persistance;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class SongRegisterCommandHandler : IRequestHandler<SongRegisterCommand>
    {
        private readonly IMapper _mapper;
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;

        public SongRegisterCommandHandler(IMapper mapper,
            ISongRepository songRepository,
            IArtistRepository artistRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
            _artistRepository = artistRepository;
        }

        public async Task Handle(SongRegisterCommand command, CancellationToken cancellationToken)
        {
            var artist = await _artistRepository.GetArtistIncludingSongs(command.ArtistName);

            if(artist == null)
            {
                throw new ArgumentException("Artist does not exist");
            }

            var song = _mapper.Map<Domain.Entities.Song>(command);

            artist.Songs.Add(song);

            await _artistRepository.CommitAsync();
        }
    }
}
