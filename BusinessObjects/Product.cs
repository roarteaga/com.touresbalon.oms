using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public partial class Product
    {
        [Key]
        public long idProduct { get; set; }

        public long? idTransport { get; set; }

        public long? idEntertainment { get; set; }

        public long? idHotel { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string urlImage { get; set; }

        public decimal price { get; set; }

        public decimal? discountRate { get; set; }

        [StringLength(50)]
        public string code { get; set; }

        public long? source_city { get; set; }

        public long? target_city { get; set; }

        public DateTime? spectacle_date { get; set; }

        public DateTime? arrival_date { get; set; }

        public DateTime? departure_date { get; set; }

        public string description { get; set; }

        public long? idUser { get; set; }
        public DateTime? modificationDate { get; set; }
    }
}
