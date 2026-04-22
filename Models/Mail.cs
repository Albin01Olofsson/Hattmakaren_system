using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Mail
    {
        public string mailId { get; set; } = string.Empty;
        public string Avsändare { get; set; } = string.Empty;
        public string Ämne { get; set; } = string.Empty;
        public string Innehåll { get; set; } = string.Empty;
        public DateTime Datum { get; set; }

    }
}
