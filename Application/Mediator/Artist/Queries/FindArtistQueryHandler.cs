using Application.InfraInterfaces.Persistance;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Artist.Queries
{
    public class FindArtistQueryHandler : IRequestHandler<FindArtistQuery, Artist.Models.FindArtistResult>
    {
        private readonly IMapper _mapper;
        private readonly IArtistRepository _artistRepository;

        public FindArtistQueryHandler(IMapper mapper,
            IArtistRepository artistRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
        }

        public async Task<Artist.Models.FindArtistResult> Handle(FindArtistQuery request, CancellationToken cancellationToken)
        {
            var artists = await _artistRepository.GetAll(x => x.Name == request.Name);

            var mappedArtist = _mapper.Map<Artist.Models.FindArtistResult>(artists.FirstOrDefault());

            return mappedArtist;
        }
    }
}
