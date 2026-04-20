using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.Views1.ViewModels
{
    public class Bokning
    {
        public int PlaneringsId { get; set; }
        public string AnvändarNamn { get; set; }
        public int OrderId { get; set; }
        public int ProduktId { get; set; }
        public string ProduktNamn { get; set; }
        public DateTime StartTid { get; set; }
        public double LangdITimmar { get; set; }


        // Hjälpmetod för att placera i rätt kolumn (Mån=1, Tis=2...)
        public int Kolumn => ((int)StartTid.DayOfWeek + 6) % 7 ;

        // Räknar ut avstånd från toppen (08:00 är start)
        public double TopPos
        {
            get
            {
                var start = TimeOnly.FromDateTime(StartTid);
                var baseTime = new TimeOnly(8, 0);

                double minutesFromStart = (start - baseTime).TotalMinutes;
                return minutesFromStart + 45;
            }
        }

        public Thickness Margin => new Thickness(LeftOffset, TopPos, 0, 0);

        public double Height => LangdITimmar * 60;

        public int Index { get; set; }

        public int AntalIKrock { get; set; }

        public double LeftOffset => Index * (120.0 / AntalIKrock);

        public double Width => 120.0 / AntalIKrock;
    }
}