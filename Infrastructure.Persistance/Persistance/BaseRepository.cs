using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Infrastructure.Persistance
{
    public abstract class BaseRepository<T> where T : class
    {
        protected readonly AppDbContext DbContext;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = DbContext.Set<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            // Mark entity and all the entities reachable through navigation properties as Modified
            DbContext.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Detach(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Detached;
        }

        public async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
