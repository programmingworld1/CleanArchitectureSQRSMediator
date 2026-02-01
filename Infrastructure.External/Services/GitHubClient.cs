using Application.InfraInterfaces;
using Application.Mediator.LibraryImporter.Models;
using System.Text.Json;

namespace Infrastructure.External.Services
{
    public class GitHubClient : IGitHubClient
    {
        private readonly HttpClient _httpClient;

        private const string ArtistsUrl =
            "https://raw.githubusercontent.com/Team-Rockstars-IT/MusicLibrary/v1.0/artists.json";

        private const string SongsUrl =
            "https://raw.githubusercontent.com/Team-Rockstars-IT/MusicLibrary/v1.0/songs.json";

        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        public GitHubClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ArtistJsonDto>> DownloadArtistsAsync()
        {
            using var stream = await _httpClient.GetStreamAsync(ArtistsUrl);

            var artists = await JsonSerializer.DeserializeAsync<List<ArtistJsonDto>>(stream, JsonOptions);

            return artists;
        }

        public async Task<List<SongJsonDto>> DownloadSongsAsync()
        {
            using var stream = await _httpClient.GetStreamAsync(SongsUrl);

            var songs = await JsonSerializer.DeserializeAsync<List<SongJsonDto>>(stream, JsonOptions);

            return songs;
        }
    }
}
