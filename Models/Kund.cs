using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Kund
    {
        [Key]
        public int KundID { get; set; }

        public string Namn { get; set; }

        public string Adress { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }
        public bool FöretagsKund { get; set; }
        public string Land { get; set; }
        public string Stad { get; set; }


        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
