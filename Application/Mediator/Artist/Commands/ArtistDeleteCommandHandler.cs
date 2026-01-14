using Application.InfraInterfaces.Persistance;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public class ArtistDeleteCommandHandler : IRequestHandler<ArtistDeleteCommand>
    {
        private readonly IMapper _mapper;
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;

        public ArtistDeleteCommandHandler(IMapper mapper,
            ISongRepository songRepository,
            IArtistRepository artistRepository)
        {
            _mapper = mapper;
            _songRepository = songRepository;
            _artistRepository = artistRepository;
        }

        public async Task Handle(ArtistDeleteCommand command, CancellationToken cancellationToken)
        {
            var artist = await _artistRepository.GetById(command.Id);

            if (artist == null)
            {
                throw new ArgumentException("Song does not exist");
            }

            _artistRepository.Delete(artist);

            await _artistRepository.CommitAsync();
        }
    }
}
