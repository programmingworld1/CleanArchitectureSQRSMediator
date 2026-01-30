using Application.Mediator.Song.Models;
using MediatR;

namespace Application.Mediator.Song.Queries
{
    public class GetSongsByGenreQuery : IRequest<GetSongsByGenreResult>
    {
        public string Genre { get; set; }
    }
}
