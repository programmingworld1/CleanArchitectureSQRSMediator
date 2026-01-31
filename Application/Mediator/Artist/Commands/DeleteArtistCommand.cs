using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public record DeleteArtistCommand(int Id) : IRequest;
}
