using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leap.BookStore.dto
{
    public class BookDto
    {
        public System.Guid book_ID { get; set; }
        public string ISBN { get; set; }
        public string book_title { get; set; }
        public System.DateTime publicationDate { get; set; }
        public System.Guid book_category_ID { get; set; }
    }
}
