using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leap.BookStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LeapBookStoreEntities DbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            DbContext = dbFactory.Init();
            
        }

        public LeapBookStoreEntities GetDataContext()
        {
            return DbContext;
        }

        public async Task<int> Complete()
        {
            return await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
