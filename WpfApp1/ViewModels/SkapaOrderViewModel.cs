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
        [ObservableProperty]
        public ObservableCollection<Kund> allaKunder = new();
        public ObservableCollection<Produkt> AllaProdukter { get; set; }
        public ObservableCollection<Produkt> TillagdaProdukter { get; set; }

        // VALDA OBJEKT (SelectedItem i rullistorna)
        [ObservableProperty] private Kund valdKund;
        [ObservableProperty] private Produkt valdProdukt;

        // ÖVRIGT
        [ObservableProperty] private decimal rabatt;
        [ObservableProperty] private string orderOversiktText;
        [ObservableProperty] private Användare inloggadAnvändare;
        [ObservableProperty] private bool isPrioVald;

        public SkapaOrderViewModel(IOrderService orderService, IAuthenticationService authService, IKundService kundService, IProduktService produktService)
        {
            _orderService = orderService;
            _kundService = kundService;
            _produktService = produktService;

            AllaKunder = new ObservableCollection<Kund>();
            AllaProdukter = new ObservableCollection<Produkt>();
            TillagdaProdukter = new ObservableCollection<Produkt>();
            
            InloggadAnvändare = Session.CurrentUser ?? new Användare { AnvändarID = 1, Namn = "Test" };

            _ = LaddaData();
        }

        [RelayCommand]
        private async Task LaddaData()
        {
            var kunderFrånDb = await _kundService.HämtaAllaKunder() ?? new List<Kund>();

            var produkterFrånDb = await _produktService.GetProdukt() ?? new List<Produkt>();

            AllaKunder.Clear();
            foreach (var k in kunderFrånDb)
            {
                AllaKunder.Add(k);
            }

            AllaProdukter.Clear();
            foreach (var p in produkterFrånDb)
            {
                AllaProdukter.Add(p);
            }

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


        partial void OnIsPrioValdChanged(bool value)
        {
            // Varje gång Judith klickar i/ur Prio, räknar vi om texten i översikten
            UppdateraOversikt();
        }

        partial void OnValdKundChanged(Kund value)
        {
            UppdateraOversikt();
        }

        [RelayCommand]
        private async Task LaggOrder()
        {
            if (ValdKund == null || !TillagdaProdukter.Any())
            {
                OrderOversiktText = "FEL: Du måste välja en kund och minst en produkt.";
                return;
            }
            if (this.Rabatt < 0)
            {
                OrderOversiktText = "Rabatt får inte vara negativ!";
                return;
            }
            try
            {
                var nyOrder = new Order
                {
                    KundID = ValdKund.KundID,
                    StartadAvID = InloggadAnvändare.AnvändarID,
                    //Produkter = TillagdaProdukter.ToList(),
                    OrderRader = TillagdaProdukter.Select(p => new OrderRad
                    {
                        ProduktID = p.ProduktID,
                        Produkt = p,
                        Antal = 1
                    }).ToList(),
                    Rabatt = this.Rabatt,
                    IsPrio = this.IsPrioVald
                };

                await _orderService.skapaOrder(nyOrder, TillagdaProdukter.Select(p => p.ProduktID).ToList());
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
            // 1. Grundsumma för alla produkter
            decimal totalt = TillagdaProdukter.Sum(p => p.Pris);
            // 2. Dra av rabatten och se till att det inte blir negativt
            decimal efterRabatt = Math.Max(0, totalt - Rabatt);
            decimal slutpris = efterRabatt;
            if (IsPrioVald)
            {
                slutpris *= 1.20m;
            }
            if (ValdKund != null && ValdKund.FöretagsKund)
            {
                slutpris *= 1.25m;
            }


            OrderOversiktText = $"Vald Kund: {(ValdKund != null ? ValdKund.Namn : "Ingen vald")}\n" +
                                $"Antal produkter: {TillagdaProdukter.Count} st\n" +
                                $"Summa: {totalt:C}\n" +
                                $"Avdragen rabatt: {Rabatt:C}\n" +
                                $"Prio-tillägg (20%): {(IsPrioVald ? "JA" : "NEJ")}\n" +
                                //$"Moms(25%) : {}\n" + 
                                $"Slutpris: {slutpris:C}";
        }


    }
}