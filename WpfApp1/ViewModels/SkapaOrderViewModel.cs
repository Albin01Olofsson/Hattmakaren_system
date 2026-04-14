using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;
using WpfApp1.Views1;

namespace WpfApp1.ViewModels
{
    public partial class SkapaOrderViewModel : ObservableObject
    {


        private readonly IOrderService _orderService;
        private readonly IAuthenticationService _authService;
        private readonly IKundService _kundService;


        //  VARIABLER FÖR GRÄNSSNITTET (Data Binding)


        [ObservableProperty]
        private Användare inloggadAnvändare;

        [ObservableProperty]
        private string kundEpost;

        // Listor för produkter
        public ObservableCollection<Produkt> AllaProdukter { get; set; }
        public ObservableCollection<Produkt> TillagdaProdukter { get; set; }

        [ObservableProperty]
        private Produkt valdProdukt;

        // Material och mått 
        public ObservableCollection<Material> material { get; set; }

        [ObservableProperty]
        private Material valtMaterial;

        [ObservableProperty]
        private decimal meterMaterial;

        [ObservableProperty]
        private decimal rabatt;

        [ObservableProperty]
        private string orderOversiktText;

        [ObservableProperty]
        private string epostSök;

        [ObservableProperty]
        private string kundDisplay;

        [ObservableProperty]
        private Kund valdKund;


        //  Konstruktor 

        public SkapaOrderViewModel(IOrderService orderService, IAuthenticationService authService, IKundService kundService)
        {
            _orderService = orderService;
            _authService = authService;
            _kundService = kundService;

            // Hämta vem som loggade in
            InloggadAnvändare = Session.CurrentUser;

            // Starta tomma listor så programmet inte kraschar
            AllaProdukter = new ObservableCollection<Produkt>();
            TillagdaProdukter = new ObservableCollection<Produkt>();
            material = new ObservableCollection<Material>();

            OrderOversiktText = "Inga produkter tillagda ännu.";
        }

        partial void OnEpostSökChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                KundDisplay = "Vänligen skriv in epost";
                ValdKund = null;
                return;
            }

            var hittadK = _kundService.HämtaAllaKunder().FirstOrDefault(k => k.Email.Equals(value));

            if (hittadK != null)
            {
                ValdKund = hittadK;
                KundDisplay = $"Kunden är medlem! \nNamn: {hittadK.Namn}";
            }
            else
            {
                ValdKund = null;
                KundDisplay = "Kunden är ny \nTryck på Ny Kund";
            }
        }

        // COMMANDS (Knapptryckningar från XAML)

        [RelayCommand]
        private void NyKund()
        {
            // Öppna ett fönster för att regga en ny kund (implementeras senare)
        }

        [RelayCommand]
        private void Specialbestallning()
        {
            // öppna ett fönster för att skapa en specialbeställning (implementeras senare)
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
            //  UI-Validering
            if (string.IsNullOrWhiteSpace(KundEpost) || !TillagdaProdukter.Any())
            {
                OrderOversiktText = "FEL: Du måste fylla i kundens e-post och lägga till minst en produkt.";
                return;
            }

            //  Hämta kund från databasen 
            var kund = _kundService.GetByEmail(KundEpost);

            if (kund == null)
            {
                OrderOversiktText = $"FEL: Hittade ingen kund med e-post '{KundEpost}'. Skapa kunden först!";
                return;
            }

            //  Bygg Order-objektet 
            var nyOrder = new Order
            {
                KundID = kund.KundID,
                StartadAvID = InloggadAnvändare.AnvändarID,
                Produkter = TillagdaProdukter.ToList(),
                Rabatt = this.Rabatt

            };

            //  Skicka till Service
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


        //  HJÄLPMETODER

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