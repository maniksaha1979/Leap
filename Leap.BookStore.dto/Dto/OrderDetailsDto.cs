using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leap.BookStore.dto
{
    public class OrderDetailsDto
    {
        public System.Guid order_Item_ID { get; set; }
        public System.Guid order_ID { get; set; }
        public System.Guid book_ID { get; set; }
        public int Item_Number { get; set; }
        public decimal Item_Agreed_Price { get; set; }
        public string Item_comment { get; set; }
    }
}
