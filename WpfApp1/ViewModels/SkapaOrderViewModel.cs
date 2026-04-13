using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    public partial class SkapaOrderViewModel : ObservableObject
    {
        // ==========================================
        // 1. INJICERADE TJÄNSTER (Kockarna)
        // ==========================================
        private readonly IOrderService _orderService;
        private readonly IAuthenticationService _authService;
        private readonly IKundService _kundService;

        // ==========================================
        // 2. VARIABLER FÖR GRÄNSSNITTET (Data Binding)
        // ==========================================

        [ObservableProperty]
        private Användare inloggadAnvändare;

        [ObservableProperty]
        private string kundEpost;

        // Listor för produkter
        public ObservableCollection<Produkt> AllaProdukter { get; set; }
        public ObservableCollection<Produkt> TillagdaProdukter { get; set; }

        [ObservableProperty]
        private Produkt valdProdukt;

        // Material och mått (Dessa matchar din XAML exakt)
        public ObservableCollection<Material> material { get; set; }

        [ObservableProperty]
        private Material valtMaterial;

        [ObservableProperty]
        private decimal meterMaterial;

        [ObservableProperty]
        private decimal rabatt;

        [ObservableProperty]
        private string orderOversiktText;

        // ==========================================
        // 3. KONSTRUKTOR (Körs när fönstret öppnas)
        // ==========================================
        public SkapaOrderViewModel(IOrderService orderService, IAuthenticationService authService, IKundService kundService)
        {
            _orderService = orderService;
            _authService = authService;
            _kundService = kundService;

            // Hämta vem som loggade in
            InloggadAnvändare = _authService.InloggadAnvändare;

            // Starta tomma listor så programmet inte kraschar
            AllaProdukter = new ObservableCollection<Produkt>();
            TillagdaProdukter = new ObservableCollection<Produkt>();
            material = new ObservableCollection<Material>();

            OrderOversiktText = "Inga produkter tillagda ännu.";
        }

        // ==========================================
        // 4. COMMANDS (Knapptryckningar från XAML)
        // ==========================================

        [RelayCommand]
        private void NyKund()
        {
            // Här kan du senare öppna ett fönster för att registrera en ny kund
        }

        [RelayCommand]
        private void Specialbestallning()
        {
            // Här kan du senare hantera specialbeställningar (t.ex. öppna ett nytt fönster)
        }

        [RelayCommand]
        private void LaggTillProdukt()
        {
            if (ValdProdukt != null)
            {
                TillagdaProdukter.Add(ValdProdukt);
                ValdProdukt = null; // Töm rullistan så den är redo för nästa val
                UppdateraOversikt();
            }
        }

        [RelayCommand]
        private void LaggTillMaterial()
        {
            if (ValtMaterial != null && MeterMaterial > 0)
            {
                UppdateraOversikt();
            }
        }

        [RelayCommand]
        private void LaggTillRabatt()
        {
            // Uppdaterar bara texten i vyn. Rabatten dras av "på riktigt" i Servicen när vi sparar.
            UppdateraOversikt();
        }

        [RelayCommand]
        private void LaggOrder()
        {
            // 1. UI-Validering
            if (string.IsNullOrWhiteSpace(KundEpost) || !TillagdaProdukter.Any())
            {
                OrderOversiktText = "FEL: Du måste fylla i kundens e-post och lägga till minst en produkt.";
                return;
            }

            // 2. Hämta kund från databasen (Säkerställ att GetByEmail finns i din IKundService)
            var kund = _kundService.GetByEmail(KundEpost);

            if (kund == null)
            {
                OrderOversiktText = $"FEL: Hittade ingen kund med e-post '{KundEpost}'. Skapa kunden först!";
                return;
            }

            // 3. Bygg Order-objektet och packa med allt från fönstret
            var nyOrder = new Order
            {
                KundID = kund.KundID,
                StartadAvID = InloggadAnvändare.AnvändarID,
                Produkter = TillagdaProdukter.ToList(),
                Rabatt = this.Rabatt
                // Har ni lagt till material i databasen kan ni lägga till: ValtMaterial = this.ValtMaterial
            };

            // 4. Skicka till Service och städa upp!
            try
            {
                _orderService.skapaOrder(nyOrder);

                // Töm fönstret för nästa kund
                TillagdaProdukter.Clear();
                KundEpost = string.Empty;
                Rabatt = 0;
                MeterMaterial = 0;
                ValtMaterial = null;

                OrderOversiktText = "Ordern har skapats och sparats i databasen!";
            }
            catch (Exception ex)
            {
                OrderOversiktText = "ETT FEL UPPSTOD: " + ex.Message;
            }
        }

        // ==========================================
        // 5. HJÄLPMETODER
        // ==========================================
        private void UppdateraOversikt()
        {
            // Visuellt pris för att Judith ska se vad det kostar medan hon bygger ordern
            decimal totalt = TillagdaProdukter.Sum(p => p.pris);
            decimal prisEfterRabatt = totalt - Rabatt;

            if (prisEfterRabatt < 0) prisEfterRabatt = 0;

            string materialText = ValtMaterial != null && MeterMaterial > 0
                ? $"\nValt material: {ValtMaterial.Namn} ({MeterMaterial} m)"
                : "";

            OrderOversiktText = $"Produkter i order: {TillagdaProdukter.Count} st.{materialText}\n" +
                                $"Rabatt: {Rabatt} kr\n" +
                                $"Preliminärt pris: {prisEfterRabatt} kr";
        }
    }
}