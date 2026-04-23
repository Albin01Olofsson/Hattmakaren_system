using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OrderRad
    {
        [Key]
        public int OrderRadID { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }

        public int ProduktID { get; set; }
        public Produkt Produkt { get; set; }

        public ICollection<Planering> Planeringar { get; set; } = new List<Planering>();

        public int Antal { get; set; }
    }
}
