using DAL;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfApp1.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private int _antalAktivaOrdrar;
        private decimal _totalIntakt; // Vi använder decimal för pengar

        public int AntalAktivaOrdrar
        {
            get => _antalAktivaOrdrar;
            set { _antalAktivaOrdrar = value; OnPropertyChanged(); }
        }

        public decimal TotalIntakt
        {
            get => _totalIntakt;
            set { _totalIntakt = value; OnPropertyChanged(); }
        }

        public async Task LaddaDataAsync()
        {
            using (var db = new DBcontext())
            {
                // 1. Hämta aktiva ordrar som inte är 'Levererad' 
                AntalAktivaOrdrar = await db.Ordrar
                    .CountAsync(o => o.Status != "Levererad");


                TotalIntakt = await db.Ordrar
                    .Where(o => o.Status == "Levererad")
                    .SumAsync(o => o.Pris);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
