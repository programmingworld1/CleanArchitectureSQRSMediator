using Domain.Entities;

namespace Application.InfraInterfaces.Persistance
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<List<Artist>> GetAllArtistsIncludingSongs();
        Task<Artist?> GetArtistIncludingSongs(string name);
        Task<Artist?> GetArtistBySongId(int songId);
        Task<List<Artist>> GetArtistsBySongIds(List<int> songIds);
    }
}
