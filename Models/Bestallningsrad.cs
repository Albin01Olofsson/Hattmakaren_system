using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BestallningsRad
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Material")]
        public int MaterialId { get; set; }
        public Material Material { get; set; } = null!;

        public int Antal { get; set; }

        [ForeignKey("Bestallning")]
        public int MaterialBeställningID { get; set; }
        public MaterialBeställning? Bestallning { get; set; }

        public decimal RadPris => Antal * Material.Pris;
    }
}
