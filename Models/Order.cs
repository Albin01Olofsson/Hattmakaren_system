using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public decimal Pris { get; set; }

        public DateTime Datum { get; set; }

        public bool Färdig { get; set; } = false;


        [ForeignKey("StartadAv")]
        public int StartadAvID { get; set; }
        public Användare StartadAv { get; set; } = null!;



        public List<Produkt> Produkter { get; set; } = new List<Produkt>();



        public Kund Kund { get; set; } = null!;

        [ForeignKey("Kund")]
        public int KundID { get; set; }

    }
}
