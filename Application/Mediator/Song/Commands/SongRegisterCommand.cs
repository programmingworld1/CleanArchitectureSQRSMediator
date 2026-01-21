using MediatR;

namespace Application.Mediator.Song.Commands
{
    public class SongRegisterCommand : IRequest
    {
        public required string Name { get; set; }
        public required string ArtistName { get; set; }
        public int Year { get; set; }
        public int Bpm { get; set; }
        public int Duration { get; set; }
        public string? Shortname { get; set; }
        public string? Genre { get; set; }
        public string? SpotifyId { get; set; }
        public string? Album { get; set; }
    }
}
