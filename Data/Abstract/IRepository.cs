using System.Linq.Expressions;

namespace SqlPersistence.Abstract
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IQueryable<T> Query();
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> criteria, bool tracking = true);
        T SingleOrDefault(Expression<Func<T, bool>> criteria);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);


    }
}
