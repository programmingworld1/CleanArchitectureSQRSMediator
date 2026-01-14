using Application.Mediator.LibraryImporter.Models;

namespace Application.InfraInterfaces
{
    public interface IGitHubClient
    {
        public Task<List<ArtistJsonDto>> DownloadArtistsAsync();
        public Task<List<SongJsonDto>> DownloadSongsAsync();
    }
}
