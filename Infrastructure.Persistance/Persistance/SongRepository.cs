using Application.InfraInterfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistance
{
    internal class SongRepository : BaseRepository<Song>, ISongRepository
    {
        public SongRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Song>> GetAllWithArtist(Expression<Func<Song, bool>> predicate)
        {
            return await DbContext.Set<Song>()
                .Include(s => s.Artist)
                .Where(predicate)
                .ToListAsync();
        }
    }
}
