using System.Linq.Expressions;

namespace LinqToDatabaseProject.Managers
{
    public interface IManager<T> where T : class
    {
        public IQueryable<T> GetAll();
        public T? GetById(int id);
        public T Create(T entity);
        public bool Delete(int id);
        public IQueryable<T> Filter(Expression<Func<T, bool>> filter);
    }
}
