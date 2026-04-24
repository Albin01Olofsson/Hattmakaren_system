using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Artikel
    {
        public int ArtikelId { get; set; }
        public string Namn { get; set; }
        public string HattTyp { get; set; }
        public string Modell { get; set; }
        public string Färg {  get; set; }
        public string Decoration { get; set; }
        public decimal Pris { get; set; }
        public ICollection<Produkt> Produkter { get; set; } = new List<Produkt>();
    }
}
