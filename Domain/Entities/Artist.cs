namespace Domain.Entities
{
    public class Artist
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        private readonly List<Song> _songs = new();
        public IReadOnlyCollection<Song> Songs => _songs.AsReadOnly();

        // EF Core
        private Artist() { }

        public Artist(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Artist name is required", nameof(name));

            Name = name.Trim();
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Artist name is required", nameof(name));

            Name = name.Trim();
        }

        public bool HasSong(string songName)
        {
            if (string.IsNullOrWhiteSpace(songName))
                return false;

            var trimmedName = songName.Trim();
            return _songs.Any(s => string.Equals(s.Name.Trim(), trimmedName, StringComparison.OrdinalIgnoreCase));
        }

        public void AddSong(Song song)
        {
            if (song is null)
                throw new ArgumentNullException(nameof(song));

            var songName = song.Name?.Trim();

            if (string.IsNullOrWhiteSpace(songName))
                throw new ArgumentException("Song name is required.", nameof(song));

            if (_songs.Any(s => string.Equals(s.Name.Trim(), songName, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Song already exists for this artist.");

            _songs.Add(song);
        }
    }
}
