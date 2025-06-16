using FleetTrackerSystem.Domain.Interfaces;
using FleetTrackerSystem.Domain.Models;
using FleetTrackerSystem.Infrastructure.Data;
using System.Linq.Expressions;

namespace FleetTrackerSystem.Infrastructure.Repositories.Repos
{
    public class GenericRepository<T> : IGeneric<T> where T : BaseClass
    {

       private readonly FeetTrackerDbContext _context;

        public GenericRepository(FeetTrackerDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);

        }

        public void Remove(int id)
        {
            var entity = GetByID(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Set<T>().Remove(entity);
            }

        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public T GetByID(int id)
        {
            return Get(x => x.ID == id).FirstOrDefault();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
