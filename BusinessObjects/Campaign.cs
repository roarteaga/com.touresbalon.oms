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
        public long idCampaign { get; set; }

        [Required]
        public string name { get; set; }

        public long idStateCampaign { get; set; }

        [Required]
        public string urlImage { get; set; }

        [Required]
        public string description { get; set; }

        public long? idProduct { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public long? idUser { get; set; }

        public DateTime? modificationDate { get; set; }

    }
}
