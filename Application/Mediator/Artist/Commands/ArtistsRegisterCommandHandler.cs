using Application.InfraInterfaces.Persistance;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public class ArtistsRegisterCommandHandler : IRequestHandler<ArtistsRegisterCommand>
    {
        private readonly IMapper _mapper;
        private readonly IArtistRepository _artistRepository;
        public ArtistsRegisterCommandHandler(IMapper mapper, 
            IArtistRepository artistRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
        }

        public async Task Handle(ArtistsRegisterCommand command, CancellationToken cancellationToken)
        {
            var domainArtists = _mapper.Map<List<Domain.Entities.Artist>>(command.Artists);

            await _artistRepository.AddRange(domainArtists);

            await _artistRepository.CommitAsync();
        }
    }
}
