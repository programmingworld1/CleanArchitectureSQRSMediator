using Application.InfraInterfaces.Persistance;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public class CreateArtistsCommandHandler : IRequestHandler<CreateArtistsCommand>
    {
        private readonly IMapper _mapper;
        private readonly IArtistRepository _artistRepository;
        public CreateArtistsCommandHandler(IMapper mapper, 
            IArtistRepository artistRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
        }

        public async Task Handle(CreateArtistsCommand command, CancellationToken cancellationToken)
        {
            var domainArtists = _mapper.Map<List<Domain.Entities.Artist>>(command.Artists);

            await _artistRepository.AddRange(domainArtists);

            await _artistRepository.CommitAsync();
        }
    }
}
