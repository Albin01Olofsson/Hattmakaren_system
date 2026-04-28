using BL.Interfaces;
using BL.Services;
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
        private readonly ITullService _tullService;
        private readonly IFraktjaktService _fraktService;

        // LISTOR (ItemsSource för rullistorna)
        [ObservableProperty]
        public ObservableCollection<Kund> allaKunder = new();
        public ObservableCollection<Produkt> AllaProdukter { get; set; }
        public ObservableCollection<Produkt> TillagdaProdukter { get; set; }

        [ObservableProperty] private ObservableCollection<FraktAlternativ> fraktVal = new();

        // VALDA OBJEKT (SelectedItem i rullistorna)
        [ObservableProperty] private Kund valdKund;
        [ObservableProperty] private Produkt valdProdukt;
        [ObservableProperty] private FraktAlternativ valtFraktAlternativ;

        // ÖVRIGT
        [ObservableProperty] private decimal rabatt;
        [ObservableProperty] private string orderOversiktText;
        [ObservableProperty] private Användare inloggadAnvändare;
        [ObservableProperty] private bool isPrioVald;
        [ObservableProperty] private decimal tullKostnad;

        public SkapaOrderViewModel(IOrderService orderService, IAuthenticationService authService, IKundService kundService, IProduktService produktService, ITullService tullService, IFraktjaktService fraktService)
        {
            _orderService = orderService;
            _kundService = kundService;
            _produktService = produktService;
            _tullService = tullService;
            _fraktService = fraktService;

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


        partial void OnValdKundChanged(Kund value)
        {
            // Så fort kunden ändras, startar vi en ny tullberäkning
            _ = RaknaUtTullAsync();
            UppdateraOversikt();
        }

        [RelayCommand]
        private void LaggTillProdukt()
        {
            if (ValdProdukt != null)
            {
                TillagdaProdukter.Add(ValdProdukt);

                // HÄR ÄR ÄNDRINGEN: Räkna om tullen nu när totalpriset har ökat!
                _ = RaknaUtTullAsync();
                UppdateraOversikt();
            }
        }

        [RelayCommand]
        private void LaggTillRabatt()
        {
            // HÄR ÄR ÄNDRINGEN: Räkna om tullen nu när priset har sänkts!
            _ = RaknaUtTullAsync();
            UppdateraOversikt();
        }


        partial void OnIsPrioValdChanged(bool value)
        {
            // Varje gång Judith klickar i/ur Prio, räknar vi om texten i översikten
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
            if (ValtFraktAlternativ == null)
            {
                OrderOversiktText = "FEL: Du måste välja ett fraktalternativ";
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

                //Ber om bokning
                var bekräftadFrakt = await _fraktService.BokaFrakt(nyOrder.OrderID, ValtFraktAlternativ);

                if (bekräftadFrakt != null)
                {
                    bekräftadFrakt.OrderID = nyOrder.OrderID;
                }

                if (string.IsNullOrEmpty(bekräftadFrakt.Status)) bekräftadFrakt.Status = "Frakt bokad";

                bekräftadFrakt.StartDatum = DateTime.Now;

                await _orderService.SparaFrakt(bekräftadFrakt);

                // Nollställ formuläret
                TillagdaProdukter.Clear();
                Rabatt = 0;
                ValdKund = null;
                TullKostnad = 0;
                UppdateraOversikt();
                OrderOversiktText = "KLART! Ordern har sparats.";
            }
            catch (Exception ex)
            {
                OrderOversiktText = "Ett fel uppstod: " + ex.Message;
            }
        }

        private async Task RaknaUtTullAsync()
        {
            if (ValdKund != null && !string.IsNullOrEmpty(ValdKund.Land))
            {
                // Vi räknar ut summan av produkterna (efter rabatt) som tullen ska baseras på
                decimal produktSumma = TillagdaProdukter.Sum(p => p.Pris);
                decimal summaAttFörtulla = Math.Max(0, produktSumma - Rabatt);

                // Anropa ditt API
                TullKostnad = await _tullService.BeraknaTullViaAPI(summaAttFörtulla, ValdKund.Land);
            }
            else
            {
                TullKostnad = 0;
            }

            // Uppdatera texten på skärmen
            UppdateraOversikt();
        }



        private void UppdateraOversikt()
        {
            // 1. Grundsumma för alla produkter
            decimal totalt = TillagdaProdukter.Sum(p => p.Pris);
            // 2. Dra av rabatten och se till att det inte blir negativt
            decimal efterRabatt = Math.Max(0, totalt - Rabatt);
            bool momsTillägg = ValdKund != null && ValdKund.FöretagsKund;
            decimal moms = Math.Max(0, totalt * 0.25m);
            decimal slutpris = efterRabatt + TullKostnad;

            if (IsPrioVald)
            {
                slutpris *= 1.20m;
            }
            if (momsTillägg)
            {
                slutpris += moms;
            }

            decimal fraktKostnad = ValtFraktAlternativ?.Pris ?? 0m;
            slutpris += fraktKostnad;


            OrderOversiktText = $"Vald Kund: {(ValdKund != null ? ValdKund.Namn : "Ingen vald")}\n" +
                        $"Land: {(ValdKund != null ? ValdKund.Land : "-")}\n" + // Visa landet också!
                        $"Antal produkter: {TillagdaProdukter.Count} st\n" +
                        $"Summa: {totalt:C}\n" +
                        $"Avdragen rabatt: {Rabatt:C}\n" +
                        $"Tullavgift (via API): {TullKostnad:C}\n" + // Visa tullen här!
                        $"Prio-tillägg (20%): {(IsPrioVald ? "JA" : "NEJ")}\n" +
                        $"Moms (25%): {(momsTillägg ? "JA" : "NEJ")}\n" +
                        $"Fraktkostnad: {fraktKostnad:C} (via {ValtFraktAlternativ?.Namn ?? "Ej vald"}\n" +
                        $"Slutpris: {slutpris:C}";
        }

        [RelayCommand]
        public async Task HämtaFraktVal()
        {
            if (ValdKund == null)
            {
                return;
            }
            var val = await _fraktService.HämtaFraktAlternativ(ValdKund.Land);
            FraktVal.Clear();
            foreach (var v in val) FraktVal.Add(v);
        }


    }
}