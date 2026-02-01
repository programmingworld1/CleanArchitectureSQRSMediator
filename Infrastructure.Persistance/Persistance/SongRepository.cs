using Application.InfraInterfaces.Persistance;
using Domain.Entities;

namespace Infrastructure.Persistance
{
    internal class SongRepository : BaseRepository<Song>, ISongRepository
    {
        public SongRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
