using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leap.BookStore.DAL.Repositories
{
    public class DbFactory : IDbFactory
    {
        LeapBookStoreEntities DbContext;

        public LeapBookStoreEntities Init()
        {
            return DbContext ?? (DbContext = new LeapBookStoreEntities());
        }
    }
}
