using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class MaterialBeställning
    {
        [Key]
        public int MaterialBeställningID { get; set; }

        public List<Material> MaterialLista { get; set; } = null!;



        public decimal TotalPris { get; set; }

        [ForeignKey("StartadAv")]
        public int StartadAvID { get; set; }
        public Användare StartadAv { get; set; } = null!;

    }
}
