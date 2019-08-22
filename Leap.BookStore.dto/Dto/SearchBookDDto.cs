using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leap.BookStore.dto
{
    public class SearchBookDDto
    {
        public Guid BookID { get; set; }
        public string ISBN { get; set; }
        public string BookTitle { get; set; }
        public string CategoryCode { get; set; }
        public string CodeDescription { get; set; }
    }
}
