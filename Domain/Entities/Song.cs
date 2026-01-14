namespace Domain.Entities
{
    public class Song
    {
        public int Id { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; } = null!;

        public string Name { get; set; }
        public int Year { get; set; }
        public string? ArtistName { get; set; }
        public string? Shortname { get; set; }
        public int Bpm { get; set; }
        public int Duration { get; set; }
        public string? Genre { get; set; }
        public string? SpotifyId { get; set; }
        public string? Album { get; set; }
    }
}
