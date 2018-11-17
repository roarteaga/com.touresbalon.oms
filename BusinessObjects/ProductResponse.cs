using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class ProductResponse
    {
        public int? totalPaginas { get; set; }
        public List<Product> products { get; set; }
    }
}
