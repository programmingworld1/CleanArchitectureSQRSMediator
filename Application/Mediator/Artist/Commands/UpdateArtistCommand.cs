using Application.ApplicationResult;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public record UpdateArtistCommand(
        int Id,
        string Name,
        byte[]? RowVersion
    ) : IRequest<Result>;
}
