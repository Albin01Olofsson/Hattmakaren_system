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

        public string Status { get; set; } = "Ej påbörjat";

        public string PlaneringsNamn { get; set; } = string.Empty;

        [ForeignKey("Användare")]
        public int AnvändarID { get; set; }
        public Användare Användare { get; set; }

        public int OrderRadID { get; set; }
        public OrderRad OrderRad { get; set; }

    }
}
