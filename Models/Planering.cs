using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Planering
    {
        [Key]
        public int PlaneringsID { get; set; }
        public DateTime StartTid { get; set; } = DateTime.Now;
        public DateTime SlutTid { get; set; } 

        [ForeignKey("Användare")]
        public int AnvändarID { get; set; }
        public Användare Användare { get; set; }

        [ForeignKey("Produkt")]
        public int ProduktID { get; set; }
        public Produkt Produkt { get; set; }

    }
}
