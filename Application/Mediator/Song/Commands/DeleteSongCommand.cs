using MediatR;
using Application.ApplicationResult;

namespace Application.Mediator.Song.Commands
{
    public class DeleteSongCommand : IRequest<Result> 
    {
        public int Id { get; set; }
    }
}
