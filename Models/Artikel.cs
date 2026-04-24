using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Artikel
    {
        [Key]
        public int ArtikelId { get; set; }
        public string ArtikelNr { get; set; }
        public string HattTyp { get; set; } = string.Empty; 
        public string Modell { get; set; } = string.Empty;
        public string Färg { get; set; } = string.Empty;
        public string Decoration { get; set; } = string.Empty;
        public int Antal { get; set; }
        public ICollection<Produkt> Produkter { get; set; } = new List<Produkt>();
    }
}
