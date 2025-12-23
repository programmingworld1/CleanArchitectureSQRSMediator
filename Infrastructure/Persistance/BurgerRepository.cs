using Application.Persistance;
using Domain.Entities;

namespace Infrastructure.Persistance
{
    public class BurgerRepository : BaseRepository<Burger>, IBurgerRepository
    {
        public BurgerRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
