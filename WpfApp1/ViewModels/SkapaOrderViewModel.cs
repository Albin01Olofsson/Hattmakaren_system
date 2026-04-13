using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels // Ändra namespace om ditt projekt heter något annat
{
    public partial class SkapaOrderViewModel : ObservableObject
    {
        private readonly IOrderService _orderService;
        private readonly IKundService _kundService;
        private readonly IAuthenticationService _authService;

        // Listor som vyn (XAML) kommer att visa
        public ObservableCollection<Kund> Kunder { get; set; }
        public ObservableCollection<Produkt> ValdaProdukter { get; set; }

        // Håller koll på vilken kund Judith har valt i rullistan
        [ObservableProperty]
        private Kund valdKund;

        // Håller koll på vem som är inloggad så vi kan visa namnet
        [ObservableProperty]
        private Användare inloggadAnvändare;

        // Konstruktor med Dependency Injection
        public SkapaOrderViewModel(IOrderService orderService, IKundService kundService, IAuthenticationService authService)
        {
            _orderService = orderService;
            _kundService = kundService;
            _authService = authService;

            // Fyll rullistan med kunder från databasen
            Kunder = new ObservableCollection<Kund>(_kundService.HämtaAllaKunder());

            // Skapa en tom lista för hattarna som ska läggas till
            ValdaProdukter = new ObservableCollection<Produkt>();

            // Hämta den inloggade användaren från inloggnings-sessionen
            InloggadAnvändare = _authService.InloggadAnvändare;
        }

        // Detta kommando kopplas till knappen "Spara Order"
        [RelayCommand]
        private void SparaOrder()
        {
            // 1. Enkel UI-validering (Har vi en kund och minst en hatt?)
            if (valdKund == null || !ValdaProdukter.Any())
            {
                // Här kan man i framtiden lägga in en MessageBox som säger "Välj kund!"
                return;
            }

            // 2. Bygg rådatan (Brickan som ska skickas till Service-kocken)
            var nyOrder = new Order
            {
                KundID = valdKund.KundID,
                StartadAvID = InloggadAnvändare.AnvändarID,
                // Vi skickar bara med listan av produkter. 
                // Datum och Pris fixar ju Servicen numera!
                Produkter = ValdaProdukter.ToList()
            };

            // 3. Skicka till Service
            _orderService.skapaOrder(nyOrder);

            // 4. Städa formuläret så det är tomt för nästa kund
            ValdaProdukter.Clear();
            valdKund = null;
        }

        // Hjälpmetod för att lägga till en hatt i ordern (kopplas till en annan knapp i UI)
        public void LäggTillHatt(Produkt nyHatt)
        {
            ValdaProdukter.Add(nyHatt);
        }
    }
}