using System.Linq.Expressions;

namespace LinqToDatabaseProject.Managers
{
    public interface IManager<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T? GetById(int id);
        public T Create(T entity);
        public bool Delete(int id);
        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter);
    }
}
