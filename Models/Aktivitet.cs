using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Aktivitet
    {
        [Key]
        public int AktivitetID { get; set; }
        public string Namn { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }

        public int SkapadAvID { get; set; } // Foreign Key
        public virtual Användare SkapadAv { get; set; } = null!;

        public virtual ICollection<Användare> Deltagare { get; set; } = new List<Användare>();
    }
}
