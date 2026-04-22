using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class SchemaBlock
    {
        public int Id { get; set; }

        public string Typ { get; set; } // "Planering" | "Aktivitet"

        public string Titel { get; set; }

        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }

        public int Kolumn { get; set; }

        public double TopPos { get; set; }
        public double Height { get; set; }

        public int Index { get; set; }
        public int AntalIKrock { get; set; }

        public string Färg { get; set; }

        public int? OrderId { get; set; }
        public int? ProduktId { get; set; }
    }
}
