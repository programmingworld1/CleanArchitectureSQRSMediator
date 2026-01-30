using Application.InfraInterfaces.Persistance;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand>
    {
        private readonly IMapper _mapper;
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;

        public DeleteSongCommandHandler(IMapper mapper,
            ISongRepository songRepository,
            IArtistRepository artistRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
            _artistRepository = artistRepository;
        }

        public async Task Handle(DeleteSongCommand command, CancellationToken cancellationToken)
        {
            var song = await _songRepository.GetById(command.Id);

            if (song == null)
            {
                throw new ArgumentException("Song does not exist");
            }

            _songRepository.Delete(song);

            await _artistRepository.CommitAsync();
        }
    }
}
