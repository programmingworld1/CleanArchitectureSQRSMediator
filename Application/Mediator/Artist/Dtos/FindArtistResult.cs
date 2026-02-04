namespace Application.Mediator.Artist.Models
{
    public class FindArtistResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? RowVersion { get; set; }
    }
}
