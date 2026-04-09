using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public double Pris { get; set; }

        public DateTime Datum { get; set; }

        public bool Färdig { get; set; } = false;

        public Användare StartadAv { get; set; } = null!;

        public Kund Beställare { get; set; } = null!;

        public List<LagerfördProdukt> Produkter { get; set; } = new List<LagerfördProdukt>();

        public List<SpecialBeställning> SpecialBeställningar { get; set; } = new List<SpecialBeställning>();

        public Kund Kund { get; set; } = null!;

        [ForeignKey("Kund")]
        public string KundID { get; set; } = null!;

    }
}
