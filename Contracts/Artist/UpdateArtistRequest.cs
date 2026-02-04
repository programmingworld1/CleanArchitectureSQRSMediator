namespace Contracts.Artist
{
    public record UpdateArtistRequest(
        string Name,
        byte[]? RowVersion
    );
}