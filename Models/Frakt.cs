using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Frakt
    {
        [Key]
        public string Sändningsnummer { get; set; }
        public int kolliId { get; set; }
        public string status { get; set; } = string.Empty;

        public Order Order { get; set; } = null!;

        [ForeignKey("Order")]
        public int OrderID { get; set; }
    }
}
