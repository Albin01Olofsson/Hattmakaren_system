using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Produkt
    {
        [Key]
        public int ProduktID { get; set; }

        public string namn { get; set; }

        public decimal pris { get; set; }

        public bool Färdig { get; set; } = false;

        public string Storlek { get; set; }

        public List<Material> MaterialLista { get; set; } = new List<Material>();


        [ForeignKey("Order")]
        public int? OrderID { get; set; }
        public Order? Order { get; set; }

        [ForeignKey("TillverkadAv")]
        public int TillverkadAVID { get; set; }

        public Användare TillverkadAv { get; set; } = null!;

    }
}
