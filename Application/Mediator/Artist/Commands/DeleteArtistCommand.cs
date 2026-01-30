using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public class DeleteArtistCommand : IRequest
    {
        public int Id { get; set; }
    }
}
