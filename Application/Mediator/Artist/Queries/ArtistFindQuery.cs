using MediatR;

namespace Application.Mediator.Artist.Queries
{
    public class ArtistFindQuery : IRequest<Models.Artist>
    {
        public string Name { get; set; }
    }
}
