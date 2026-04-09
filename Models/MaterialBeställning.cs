using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class MaterialBeställning
    {
        [Key]
        public int MaterialBeställningID { get; set; }

        public List<Material> MaterialLista { get; set; } = null!;



        public double TotalPris { get; set; }


    }
}
