using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Song
    {
        public int Id { get; private set; }
        public Artist Artist { get; private set; }
        public string Name { get; private set; }
        public Year Year { get; private set; }
        public Bpm Bpm { get; private set; }
        public Duration Duration { get; private set; }

        public string? Shortname { get; private set; }
        public string? Genre { get; private set; }
        public string? SpotifyId { get; private set; }
        public string? Album { get; private set; }

        // EF Core only
        private Song() { }

        public Song(
            string name,
            Year year,
            Bpm bpm,
            Duration duration,
            string? shortname = null,
            string? genre = null,
            string? spotifyId = null,
            string? album = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Song name is required.", nameof(name));

            Name = name;
            Year = year;
            Bpm = bpm;
            Duration = duration;

            Shortname = shortname;
            Genre = genre;
            SpotifyId = spotifyId;
            Album = album;
        }
    }
}
