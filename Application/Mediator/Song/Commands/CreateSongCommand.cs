using MediatR;

namespace Application.Mediator.Song.Commands
{
    public record CreateSongCommand(
        string Name,
        string ArtistName,
        int Year,
        int Bpm,
        int Duration,
        string? Shortname,
        string? Genre,
        string? SpotifyId,
        string? Album
    ) : IRequest;
}
