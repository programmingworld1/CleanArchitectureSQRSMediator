using MediatR;
using Application.ApplicationResult;

namespace Application.Mediator.Song.Commands
{
    public record DeleteSongCommand(int Id) : IRequest<Result>;
}
