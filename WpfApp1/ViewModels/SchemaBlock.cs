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

        public int Kolumn { get; set; } // dag i veckan (0-4)
        public int DagIndex { get; set; } // samma som Kolumn egentligen

        public double TopPos { get; set; }

        public double Height { get; set; } // i minuter (1 min = 1 px om du kör 60px/h)

        public string Färg { get; set; }

        public int ZIndex { get; set; }

        public int? OrderId { get; set; }
        public int? ProduktId { get; set; }

        public string AnvändarNamn { get; set; }
        public string ProduktNamn { get; set; }
        //public int Id { get; set; }
        //public int ZIndex { get; set; }
        //public string? Typ { get; set; } // "Planering" | "Aktivitet"
        //public string? Titel { get; set; }
        //public DateTime StartTid { get; set; }
        //public DateTime SlutTid { get; set; }
        //public int Kolumn { get; set; }
        //public double TopPos { get; set; }
        //public double Height { get; set; }
        //public double LeftOffset => Kolumn * 150;
        //public double Width => 140;
        //public Thickness Margin => new Thickness(0, TopPos, 0, 0);
        //public string? Färg { get; set; }
        //public int? OrderId { get; set; }
        //public int? ProduktId { get; set; }
    }
}
