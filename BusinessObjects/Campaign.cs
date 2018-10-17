using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Campaign
    {
        [Key]
        public string idCampaign { get; set; }
        public string name { get; set; }
        public string idStateCampaign { get; set; }
        public string urlImage { get; set; }
        public string description { get; set; }
        public string idProduct { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }


    }
}
