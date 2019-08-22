using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Leap.BookStore.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(Guid id);

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}
