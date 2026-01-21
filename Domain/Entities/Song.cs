namespace Domain.Entities
{
    public class Song
    {
        public int Id { get; private set; }

        public int ArtistId { get; private set; }
        public Artist Artist { get; private set; }

        public string Name { get; private set; }
        public int Year { get; private set; }
        public int Bpm { get; private set; }
        public int Duration { get; private set; }

        public string? Shortname { get; private set; }
        public string? Genre { get; private set; }
        public string? SpotifyId { get; private set; }
        public string? Album { get; private set; }

        // EF Core only
        private Song() { }

        public Song(
            int artistId,
            string name,
            int year,
            int bpm,
            int duration,
            string? shortname = null,
            string? genre = null,
            string? spotifyId = null,
            string? album = null)
        {
            // All of the business logic related validation.
            // The other validations are at DTO level with fluent validation, or in the DB (via for example MaxCount).
            if (artistId <= 0)
                throw new ArgumentException("ArtistId must be valid.", nameof(artistId));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Song name is required.", nameof(name));

            if (bpm <= 0)
                throw new ArgumentOutOfRangeException(nameof(bpm));

            if (duration <= 0)
                throw new ArgumentOutOfRangeException(nameof(duration));

            ArtistId = artistId;
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
