using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class DeleteSongCommand : IRequest
    {
        public int Id { get; set; }
    }
}
