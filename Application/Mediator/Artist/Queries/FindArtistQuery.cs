using Application.ApplicationResult;
using MediatR;

namespace Application.Mediator.Artist.Queries
{
    public record FindArtistQuery(string Name) : IRequest<Result<Models.FindArtistResult>>;
}
