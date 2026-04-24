using System.Windows.Media;
namespace WpfApp1.ViewModels
{
    public class SchemaBlock
    {
        // 1. Identifiering
        public int Id { get; set; }
        public string Typ { get; set; } // "Planering" eller "Aktivitet"

        // 2. Tider & Visning (Det Syncfusion behöver för kalendern)
        public string Titel { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public Brush Färg { get; set; }
        public bool ÄrHeldag { get; set; }

        // 3. Affärsdata (Det vi visar i ToolTip-rutan)
        public int? OrderId { get; set; }
        public int? ProduktId { get; set; }
        public string AnvändarNamn { get; set; }
        public string ProduktNamn { get; set; }
    }
}