using Application.InfraInterfaces.Persistance;
using Application.Mediator.Song.Models;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Mediator.Song.Queries
{
    public class GetSongsByGenreQueryHandler : IRequestHandler<GetSongsByGenreQuery, GetSongsByGenreResult>
    {
        private readonly ISongRepository _songRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public GetSongsByGenreQueryHandler(
            ISongRepository songRepository, 
            IMapper mapper,
            IMemoryCache cache)
        {
            _songRepository = songRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<GetSongsByGenreResult> Handle(GetSongsByGenreQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"songs:genre:{request.Genre.ToLowerInvariant()}";

            // 1️⃣ Eerst cache checken
            if (_cache.TryGetValue(cacheKey, out GetSongsByGenreResult cached))
            {
                return cached;
            }

            var songs = await _songRepository.GetAllWithArtist(s => s.Genre == request.Genre);

            var songDtos = _mapper.Map<List<SongDto>>(songs);

            var response = new GetSongsByGenreResult()
            {
                Songs = songDtos
            };

            _cache.Set(
                cacheKey,
                response,
                TimeSpan.FromMinutes(5)
            );

            return response;
        }
    }
}
