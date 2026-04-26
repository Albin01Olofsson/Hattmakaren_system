using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Reklamation
    {
        [Key]
        public int ReklamationID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }
        public Order Order { get; set; } = null!;

        [ForeignKey("Produkt")]
        public int? ProduktID { get; set; }
        public Produkt? Produkt { get; set; }

        [ForeignKey("Kund")]
        public int KundID { get; set; }
        public Kund Kund { get; set; } = null!;

        public string Orsak { get; set; } = string.Empty;
        public string Beskrivning { get; set; } = string.Empty;
        public string Status { get; set; } = "Ny";
        public string Atgard { get; set; } = string.Empty;

        public DateTime SkapadDatum { get; set; } = DateTime.Now;
        public DateTime? AvslutadDatum { get; set; }

        [ForeignKey("SkapadAv")]
        public int SkapadAvID { get; set; }
        public Användare SkapadAv { get; set; } = null!;
    }
}
