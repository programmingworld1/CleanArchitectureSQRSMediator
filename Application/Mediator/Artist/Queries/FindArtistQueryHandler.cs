using Application.ApplicationResult;
using Application.InfraInterfaces.Persistance;
using MapsterMapper;
using MediatR;

namespace Application.Mediator.Artist.Queries
{
    public class FindArtistQueryHandler : IRequestHandler<FindArtistQuery, Result<Models.FindArtistResult>>
    {
        private readonly IMapper _mapper;
        private readonly IArtistRepository _artistRepository;

        public FindArtistQueryHandler(IMapper mapper,
            IArtistRepository artistRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
        }

        public async Task<Result<Models.FindArtistResult>> Handle(FindArtistQuery request, CancellationToken cancellationToken)
        {
            var artists = await _artistRepository.GetAll(x => x.Name == request.Name);

            var artist = artists.FirstOrDefault();

            // Null = Failure, niet Success
            if (artist == null)
            {
                return Result<Models.FindArtistResult>.Failure(
                    new Error("ArtistNotFound", $"No artist found with name '{request.Name}'"));
            }

            var mappedArtist = _mapper.Map<Models.FindArtistResult>(artist);

            return Result<Models.FindArtistResult>.Success(mappedArtist);
        }
    }
}
