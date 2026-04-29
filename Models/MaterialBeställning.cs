using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class MaterialBeställning
    {
        [Key]
        public int MaterialBeställningID { get; set; }

        public DateTime? Datum { get; set; }

        public List<Material> MaterialLista { get; set; } = null!;

        public List<BestallningsRad> Rader { get; set; } = new();

        public string Leverantör {  get; set; }
        public bool Levererad { get; set; } = false;

        //public int Antal { get; set; }

        public decimal TotalPris { get; set; }

        [ForeignKey("StartadAv")]
        public int StartadAvID { get; set; }
        public Användare StartadAv { get; set; } = null!;

    }
}
