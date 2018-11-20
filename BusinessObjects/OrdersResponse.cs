using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class OrdersResponse
    {
        public int? totalPaginas { get; set; }
        public List<Order> orders { get; set; }
    }
}
