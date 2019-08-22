using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Leap.BookStore.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> Entities;

        public Repository(DbContext context)
        {
            Entities = context.Set<T>();
        }

        public async Task<T> GetById(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await Entities.Where(predicate).ToListAsync();
        }

        public void Add(T entity)
        {
            Entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Entities.AddRange(entities);
        }

        public void Remove(T entity)
        {
            Entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Entities.RemoveRange(entities);
        }
    }
}
