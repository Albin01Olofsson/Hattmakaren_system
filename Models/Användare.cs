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
    }
}
