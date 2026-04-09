using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Produkt
    {
        [Key]
        public int ProduktID { get; set; }

        public string namn { get; set; }

        public double pris { get; set; }



        public string Storlek { get; set; }

        public List<Material> MaterialLista { get; set; } = new List<Material>();


        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Order Order { get; set; }


    }
}
