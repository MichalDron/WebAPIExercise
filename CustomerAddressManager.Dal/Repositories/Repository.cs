using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerAddressManager.Dal.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return this.dbSet.AsNoTracking();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return this.dbSet.AsNoTracking().Where(expression);
        }

        public T GetSingle(Expression<Func<T, bool>> expression)
        {
            return this.dbSet.AsNoTracking().Where(expression).SingleOrDefault();
        }

        public void Create(T entity)
        {
            this.dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            this.dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
