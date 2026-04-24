using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Produkt
    {
        [Key]
        public int ProduktID { get; set; }
        public string Namn { get; set; }
        public decimal Pris { get; set; }
        public bool Färdig { get; set; } = false;
        public string Storlek { get; set; }
        public List<Material> MaterialLista { get; set; } = new List<Material>();

        public string HattTyp { get; set; } = string.Empty; //Ex: Fedora, keps, basker, typ som skor har sneakers

        public string Modell { get; set; } = string.Empty; //Typ ett namn otto har gett en generell design, typ som skor har Airmax

        public string Färg { get; set; } = string.Empty;

        public string Decoration { get; set; } = string.Empty; //typ rosett, tygblomma

        [ForeignKey("Artikel")]
        public int ArtikelID { get; set; }
        public Artikel Artikel { get; set; }
        //public ICollection<OrderRad> OrderRader { get; set; } = new List<OrderRad>();

        [ForeignKey("TillverkadAv")]
        public int TillverkadAVID { get; set; }
        public Användare TillverkadAv { get; set; } = null!;
        public bool ÄrReserverad { get; set; } = false;

        public int Lagerantal { get; set; }

    }
}
