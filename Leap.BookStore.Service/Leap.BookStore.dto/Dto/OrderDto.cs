using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leap.BookStore.dto
{
    public class OrderDto
    {
        public OrderDto()
        {
            DetailsItem = new List<OrderDetailsDto>();
        }

        public System.Guid order_ID { get; set; }
        public System.Guid customer_ID { get; set; }
        public System.DateTime order_date { get; set; }
        public decimal order_Value { get; set; }

        public List<OrderDetailsDto> DetailsItem { get; set; }
    }
}
