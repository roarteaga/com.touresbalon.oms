using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class CampaignResponse
    {
        public int? totalPaginas { get; set; }
        public List<Campaign> campaigns { get; set; }
    }
}
