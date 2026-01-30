using Application.Mediator.Artist.Models;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public class CreateArtistsCommand : IRequest
    {
        public List<CreateArtistItem> Artists { get; set; }
    }
}
