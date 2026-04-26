using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
namespace Models
{
    public class Order : INotifyPropertyChanged
    {
        [Key]
        public int OrderID { get; set; }

        public string Varukod { get; set; } = string.Empty;
        public decimal Pris { get; set; }

        public double? Moms { get; set; }
        public DateTime Datum { get; set; }

        private bool _färdig;
        public bool Färdig
        {
            get => _färdig;
            set
            {
                if(_färdig != value)
                {
                    _färdig = value;
                    OnPropertyChanged();
                }
            }
        }

        public decimal Rabatt { get; set; } = 0;

        public bool IsSpecialbeställning { get; set; } = false;

        public bool IsPrio { get; set; } = false;


        [ForeignKey("StartadAv")]
        public int StartadAvID { get; set; }
        public Användare StartadAv { get; set; } = null!;

        public string Status { get; set; } = "Ej påbörjat";

        public List<OrderRad> OrderRader { get; set; } = new();

        public DateTime FörväntadTillverkningsTid { get; set; }

        public ObservableCollection<Frakt> Frakt { get; set; } = new();
        public Kund Kund { get; set; } = null!;

        [ForeignKey("Kund")]
        public int KundID { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyNamn = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyNamn));
        }
    }
}
