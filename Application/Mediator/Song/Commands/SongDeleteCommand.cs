using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class SongDeleteCommand : IRequest
    {
        public int Id { get; set; }
    }
}
