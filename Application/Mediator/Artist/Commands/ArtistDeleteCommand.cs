using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public class ArtistDeleteCommand : IRequest
    {
        public int Id { get; set; }
    }
}
