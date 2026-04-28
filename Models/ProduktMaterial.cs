using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ProduktMaterial
    {
        [Key]
        public int ProduktMaterialID { get; set; }
        [ForeignKey("Produkt")]
        public int ProduktID { get; set; }
        public Produkt Produkt { get; set; }

        [ForeignKey("Material")]
        public int MaterialID { get; set; }
        public Material Material { get; set; }

        public decimal Mängd { get; set; }
    }
}
