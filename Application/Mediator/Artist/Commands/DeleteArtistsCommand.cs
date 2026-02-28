using Application.ApplicationResult;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public record DeleteArtistsCommand(List<int> Ids) : IRequest<Result>;
}
