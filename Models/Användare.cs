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
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; } = true;
        
        public List<MaterialBeställning> materialBeställningsLista { get; set; } = new List<MaterialBeställning>();

        public List<Order> orderLista { get; set; } = new List<Order>();

        public List<Produkt> produktLista { get; set; } = new List<Produkt>();

        public ICollection<Planering> Planeringar { get; set; } = new List<Planering>();
        
        // Koppling till de aktiviteter man har skapat (1:N)
        public virtual ICollection<Aktivitet> SkapadeAktiviteter { get; set; } = new List<Aktivitet>();

        // Koppling till de aktiviteter man deltar i (N:N)
        public virtual ICollection<Aktivitet> DeltarIAktiviteter { get; set; } = new List<Aktivitet>();
    }
}
