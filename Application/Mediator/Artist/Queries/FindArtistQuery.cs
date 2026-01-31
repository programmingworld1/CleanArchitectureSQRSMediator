using MediatR;

namespace Application.Mediator.Artist.Queries
{
    public record FindArtistQuery(string Name) : IRequest<Models.FindArtistResult>;
}
