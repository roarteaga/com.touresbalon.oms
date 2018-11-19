using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects
{
    public partial class Order
    {
        [Key]
        public long idOrder { get; set; }

        public long idStateOrder { get; set; }

        [Column(TypeName = "money")]
        public decimal amount { get; set; }

        public long idCustomer { get; set; }

        public long numberCard { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime? modificationDate { get; set; }

        [StringLength(60)]
        public string country { get; set; }

        [StringLength(60)]
        public string city { get; set; }

        public long? IdUser { get; set; }

    }
}
