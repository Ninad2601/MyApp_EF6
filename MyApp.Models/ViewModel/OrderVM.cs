using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Models.ViewModel
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public OrderDetail OrderDetail { get; set; }
        IEnumerable<OrderDetail> orderDetails { get; set; }
    }
}
