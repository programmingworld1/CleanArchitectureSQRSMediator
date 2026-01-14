using Application.InfraInterfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Artist>> GetAllArtistsIncludingSongs()
        {
            return await DbContext.Artists.Include(a => a.Songs).ToListAsync();
        }

        public async Task<Artist> GetArtistIncludingSongs(string name)
        {
            return await DbContext
                .Artists
                .Include(a => a.Songs)
                .Where(a => a.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}
