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

        //Tillagda properties för att följa kraven i "KM_Scrum_vt26.pptx"
        public string HattTyp { get; set; } = string.Empty; //Ex: Fedora, keps, basker, typ som skor har sneakers

        public string Modell { get; set; } = string.Empty; //Typ ett namn otto har gett en generell design, typ som skor har Airmax

        public string Färg { get; set; } = string.Empty;

        public string Decoration { get; set; } = string.Empty; //typ rosett, tygblomma

        //[ForeignKey("Order")]
        //public int? OrderID { get; set; }
        //public Order? Order { get; set; }

        public List<Order> Ordrar { get; set; } = new();

        [ForeignKey("TillverkadAv")]
        public int TillverkadAVID { get; set; }
        public Användare TillverkadAv { get; set; } = null!;

        public int Lagerantal { get; set; }

        public ICollection<Planering> Planeringar { get; set; } = new List<Planering>();
    }
}
