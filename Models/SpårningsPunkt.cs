using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public partial class SpårningsPunkt 
    {
        public DateTime Tidpunkt { get; set; }
        public string Plats { get; set; }
        public string Meddelande { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}
