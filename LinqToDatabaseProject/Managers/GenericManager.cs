using LinqToDatabaseProject.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LinqToDatabaseProject.Managers
{
    public class GenericManager<T> : IManager<T> where T : class
    {
        protected readonly GameDbContext _ctx;
        protected readonly DbSet<T> _dbSet;

        public GenericManager(GameDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public IQueryable<T> GetAll() => _dbSet;

        public T? GetById(int id) => _dbSet.Find(id);
        
        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }
        public bool Delete(int id)
        {
            T? entity = GetById(id);
            if (entity == null)
                return false;
            _dbSet.Remove(entity);
            return _ctx.SaveChanges() > 0;
        }
        public IQueryable<T> Filter(Expression<Func<T, bool>> filter) => _dbSet.Where(filter);
    }
}
