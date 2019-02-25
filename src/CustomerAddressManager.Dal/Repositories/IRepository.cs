using System;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerAddressManager.Dal.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        IQueryable<T> Get(Expression<Func<T, bool>> expression);

        T GetSingle(Expression<Func<T, bool>> expression);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Save();
    }
}
