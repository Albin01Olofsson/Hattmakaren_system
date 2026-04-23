using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.ViewModels
{
    public class SchemaBlock
    {
        public int Id { get; set; }
        public string Typ { get; set; } // Planering | Aktivitet
        public string Titel { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }

        public int Kolumn { get; set; } 
        public double Top { get; set; }
        public double Height { get; set; }

        public string Färg { get; set; }
        public int ZIndex { get; set; }
        public int? OrderId { get; set; }
        public int? ProduktId { get; set; }
        public int? AnvändarId { get; set; }
        public string AnvändarNamn { get; set; }
        public string ProduktNamn { get; set; }
        public string InfoText { get; set; }
        public bool IsAktivitet => Typ == "Aktivitet";
        public bool IsPlanering => Typ == "Planering";
    }
}
