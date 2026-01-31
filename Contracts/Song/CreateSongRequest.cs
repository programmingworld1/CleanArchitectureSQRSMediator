namespace Contracts.Song
{
    public record CreateSongRequest(
        string Name, 
        int Year, 
        string? ArtistName, 
        string ?Shortname, 
        int Bpm, 
        int Duration, 
        string? Genre, 
        string? SpotifyId, 
        string? Album);
}
