using System.Linq.Expressions;

namespace FleetTrackerSystem.Domain.Interfaces
{
    public interface IGeneric<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(int id);
        T GetByID(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> expression);

        Task SaveChangesAsync();
    }
}
