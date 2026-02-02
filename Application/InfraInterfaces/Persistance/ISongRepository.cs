using Domain.Entities;
using System.Linq.Expressions;

namespace Application.InfraInterfaces.Persistance
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<IEnumerable<Song>> GetAllWithArtist(Expression<Func<Song, bool>> predicate);
    }
}
