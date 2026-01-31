using Application.Mediator.Artist.Models;
using MediatR;

namespace Application.Mediator.Artist.Commands
{
    public record CreateArtistsCommand(List<CreateArtistItem> Artists) : IRequest;
}
