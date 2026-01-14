using Application.InfraInterfaces;
using Application.InfraInterfaces.Persistance;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mediator.Artist.Queries
{
    public class ArtistFindQueryHandler : IRequestHandler<ArtistFindQuery, Artist.Models.Artist>
    {
        private readonly IMapper _mapper;
        private readonly IArtistRepository _artistRepository;

        public ArtistFindQueryHandler(IMapper mapper,
            IArtistRepository artistRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
        }

        public async Task<Artist.Models.Artist> Handle(ArtistFindQuery request, CancellationToken cancellationToken)
        {
            var artists = await _artistRepository.GetAll(x => x.Name == request.Name);

            var mappedArtist = _mapper.Map<Artist.Models.Artist>(artists.FirstOrDefault());

            return mappedArtist;
        }
    }
}
