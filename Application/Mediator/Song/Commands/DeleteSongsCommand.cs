using Application.ApplicationResult;
using MediatR;

namespace Application.Mediator.Song.Commands
{
    public record DeleteSongsCommand(List<int> Ids) : IRequest<Result>;
}
