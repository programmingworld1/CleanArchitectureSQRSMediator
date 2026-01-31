using Application.Mediator.Song.Models;
using MediatR;

namespace Application.Mediator.Song.Queries
{
    public record GetSongsByGenreQuery(string Genre) : IRequest<GetSongsByGenreResult>;
}
