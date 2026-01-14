using Application.Mediator.Artist.Models;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public class ArtistsRegisterCommand : IRequest
    {
        public List<Artist.Models.ArtistRegisterItem> Artists { get; set; }
    }
}
