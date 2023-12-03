using SqlPersistence.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SqlPersistence.Persistence
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> criteria, bool tracking = true)
        {
            if (tracking)
                return Context.Set<T>().Where(criteria);
            return Context.Set<T>().AsNoTracking().Where(criteria);
        }

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public IQueryable<T> Query()
        {
            return Context.Set<T>().AsQueryable();
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> criteria)
        {
            return Context.Set<T>().SingleOrDefault(criteria);
        }
    }
}
