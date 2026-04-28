using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }

        public string Namn { get; set; }

        public decimal Pris { get; set; }

        public string Beskrivning { get; set; }

        public MåttTyp MåttTyp { get; set; }

        public int Lagerantal { get; set; }

        public ICollection<ProduktMaterial> ProduktMaterial { get; set; }
        = new List<ProduktMaterial>();

        [NotMapped]
        public string MåttText => MåttTyp switch
        {
            MåttTyp.Meter => "m",
            MåttTyp.Centimeter => "cm",
            MåttTyp.Millimeter => "mm",
            MåttTyp.Styck => "st",
            _ => ""
        };
    }
}

