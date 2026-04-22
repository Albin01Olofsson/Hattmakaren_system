using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }

        public string Namn { get; set; }

        public decimal Pris { get; set; }

        public string Beskrivning { get; set; }

        public string Typ { get; set; }

        public string Mått { get; set; }

        public int Lagerantal { get; set; }

    }
}
