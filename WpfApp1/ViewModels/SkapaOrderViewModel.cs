using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    public partial class SkapaOrderViewModel : ObservableObject
    {
        private readonly IOrderService _orderService;
        private readonly IKundService _kundService;
        private readonly IProduktService _produktService;

        // LISTOR (ItemsSource för rullistorna)
        public ObservableCollection<Kund> AllaKunder { get; set; }
        public ObservableCollection<Produkt> AllaProdukter { get; set; }
        public ObservableCollection<Produkt> TillagdaProdukter { get; set; }

        // VALDA OBJEKT (SelectedItem i rullistorna)
        [ObservableProperty] private Kund valdKund;
        [ObservableProperty] private Produkt valdProdukt;

        // ÖVRIGT
        [ObservableProperty] private decimal rabatt;
        [ObservableProperty] private string orderOversiktText;
        [ObservableProperty] private Användare inloggadAnvändare;

        public SkapaOrderViewModel(IOrderService orderService, IAuthenticationService authService, IKundService kundService, IProduktService produktService)
        {
            _orderService = orderService;
            _kundService = kundService;
            _produktService = produktService;

            // Hämta data från databasen direkt vid start
            var kunderFrånDb = _kundService.HämtaAllaKunder() ?? new System.Collections.Generic.List<Kund>();
            var produkterFrånDb = _produktService.GetProdukt() ?? new System.Collections.Generic.List<Produkt>();

            AllaKunder = new ObservableCollection<Kund>(kunderFrånDb);
            AllaProdukter = new ObservableCollection<Produkt>(produkterFrånDb);
            TillagdaProdukter = new ObservableCollection<Produkt>();

            InloggadAnvändare = Session.CurrentUser ?? new Användare { AnvändarID = 1, Namn = "Test" };

            UppdateraOversikt();
        }

        [RelayCommand]
        private void LaggTillProdukt()
        {
            if (ValdProdukt != null)
            {
                TillagdaProdukter.Add(ValdProdukt);
                UppdateraOversikt();
            }
        }

        [RelayCommand]
        private void LaggTillRabatt()
        {
            // Priset räknas om här när man trycker på knappen
            UppdateraOversikt();
        }

        [RelayCommand]
        private void LaggOrder()
        {
            if (ValdKund == null || !TillagdaProdukter.Any())
            {
                OrderOversiktText = "FEL: Du måste välja en kund och minst en produkt.";
                return;
            }

            try
            {
                var nyOrder = new Order
                {
                    KundID = ValdKund.KundID,
                    StartadAvID = InloggadAnvändare.AnvändarID,
                    Produkter = TillagdaProdukter.ToList(),
                    Rabatt = this.Rabatt
                };

                _orderService.skapaOrder(nyOrder);
                OrderOversiktText = "KLART! Ordern har sparats.";

                // Nollställ formuläret
                TillagdaProdukter.Clear();
                Rabatt = 0;
                ValdKund = null;
                UppdateraOversikt();
            }
            catch (Exception ex)
            {
                OrderOversiktText = "Ett fel uppstod: " + ex.Message;
            }
        }

        private void UppdateraOversikt()
        {
            decimal totalt = TillagdaProdukter.Sum(p => p.pris);
            decimal slutpris = Math.Max(0, totalt - Rabatt);

            OrderOversiktText = $"Vald Kund: {(ValdKund != null ? ValdKund.Namn : "Ingen valda")}\n" +
                                $"Antal produkter: {TillagdaProdukter.Count} st\n" +
                                $"Summa: {totalt:C}\n" +
                                $"Avdragen rabatt: {Rabatt:C}\n" +
                                $"Slutpris: {slutpris:C}";
        }
    }
}