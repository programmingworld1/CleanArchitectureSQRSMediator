using System.Linq.Expressions;

namespace Application.InfraInterfaces.Persistance
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Detach(T entity);
        Task CommitAsync();
    }
}
