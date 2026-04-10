using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Användare
    {
        [Key]
        public int AnvändarID { get; set; }

        public string Namn { get; set; }

        public string Telefon { get; set; }

        public string Email { get; set; }

        public string Lösenord { get; set; }

        public List<MaterialBeställning> materialBeställningsLista { get; set; } = new List<MaterialBeställning>();

        public List<Order> orderLista { get; set; } = new List<Order>();

        public List<Produkt> produktLista { get; set; } = new List<Produkt>();


    }
}
