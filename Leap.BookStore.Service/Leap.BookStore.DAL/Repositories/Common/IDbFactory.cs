using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leap.BookStore.DAL.Repositories
{
    
    public interface IDbFactory
    {
        LeapBookStoreEntities Init();
    }
}
