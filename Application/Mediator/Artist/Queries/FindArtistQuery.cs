using MediatR;

namespace Application.Mediator.Artist.Queries
{
    public class FindArtistQuery : IRequest<Models.FindArtistResult>
    {
        public string Name { get; set; }
    }
}
