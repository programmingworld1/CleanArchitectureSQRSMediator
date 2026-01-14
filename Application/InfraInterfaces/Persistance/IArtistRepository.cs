using Domain.Entities;

namespace Application.InfraInterfaces.Persistance
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<List<Artist>> GetAllArtistsIncludingSongs();
        Task<Artist> GetArtistIncludingSongs(string name);
    }
}
