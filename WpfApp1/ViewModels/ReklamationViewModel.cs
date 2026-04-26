using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1.ViewModels
{
    public partial class ReklamationViewModel : ObservableObject
    {
        private readonly IReklamationService _reklamationService;
        private readonly IOrderService _orderService;
        private List<Reklamation> _allaReklamationer = new();

        public ObservableCollection<Reklamation> Reklamationer { get; } = new();
        public ObservableCollection<Order> Ordrar { get; } = new();
        public ObservableCollection<Produkt> ProdukterIOrder { get; } = new();

        public ObservableCollection<string> StatusAlternativ { get; } = new()
        {
            "Ny",
            "Under behandling",
            "Godkänd",
            "Nekad",
            "Åtgärdad"
        };

        public ObservableCollection<string> FilterAlternativ { get; } = new()
        {
            "Alla",
            "Ny",
            "Under behandling",
            "Godkänd",
            "Nekad",
            "Åtgärdad"
        };

        public ObservableCollection<string> OrsaksAlternativ { get; } = new()
        {
            "Fel storlek",
            "Skadad produkt",
            "Fel modell",
            "Fel färg",
            "Sen leverans",
            "Annat"
        };

        public ObservableCollection<string> AtgardsAlternativ { get; } = new()
        {
            "Reparation",
            "Ny hatt",
            "Återbetalning",
            "Rabatt",
            "Ingen åtgärd"
        };

        [ObservableProperty]
        private string sokText = string.Empty;

        [ObservableProperty]
        private string valdFilterStatus = "Alla";

        [ObservableProperty]
        private Reklamation? valdReklamation;

        [ObservableProperty]
        private Order? valdOrder;

        [ObservableProperty]
        private Produkt? valdProdukt;

        [ObservableProperty]
        private string valdOrsak = "Fel storlek";

        [ObservableProperty]
        private string beskrivning = string.Empty;

        [ObservableProperty]
        private string valdStatus = "Ny";

        [ObservableProperty]
        private string valdAtgard = "Reparation";

        [ObservableProperty]
        private string kundText = string.Empty;

        [ObservableProperty]
        private string orderDetaljText = string.Empty;

        [ObservableProperty]
        private int nyaAntal;

        [ObservableProperty]
        private int underBehandlingAntal;

        [ObservableProperty]
        private int godkandaAntal;

        [ObservableProperty]
        private int atgardadeAntal;

        public ReklamationViewModel(IReklamationService reklamationService, IOrderService orderService)
        {
            _reklamationService = reklamationService;
            _orderService = orderService;
            _ = LaddaData();
        }

        partial void OnSokTextChanged(string value) => FiltreraReklamationer();

        partial void OnValdFilterStatusChanged(string value) => FiltreraReklamationer();

        partial void OnValdOrderChanged(Order? value)
        {
            ProdukterIOrder.Clear();
            ValdProdukt = null;

            if (value == null)
            {
                KundText = string.Empty;
                OrderDetaljText = string.Empty;
                return;
            }

            KundText = value.Kund?.Namn ?? string.Empty;
            var antalProdukter = value.OrderRader.Sum(or => or.Antal);
            OrderDetaljText = $"Datum: {value.Datum:yyyy-MM-dd}  |  Pris: {value.Pris:0} kr  |  Produkter: {antalProdukter}";

            foreach (var orderRad in value.OrderRader)
            {
                if (orderRad.Produkt != null && !ProdukterIOrder.Any(p => p.ProduktID == orderRad.ProduktID))
                {
                    ProdukterIOrder.Add(orderRad.Produkt);
                }
            }
        }

        partial void OnValdReklamationChanged(Reklamation? value)
        {
            if (value == null)
            {
                return;
            }

            ValdOrder = Ordrar.FirstOrDefault(o => o.OrderID == value.OrderID);
            ValdProdukt = ProdukterIOrder.FirstOrDefault(p => p.ProduktID == value.ProduktID);
            ValdOrsak = value.Orsak;
            Beskrivning = value.Beskrivning;
            ValdStatus = value.Status;
            ValdAtgard = string.IsNullOrWhiteSpace(value.Atgard) ? "Reparation" : value.Atgard;
            KundText = value.Kund?.Namn ?? ValdOrder?.Kund?.Namn ?? string.Empty;
        }

        private async Task LaddaData()
        {
            Ordrar.Clear();
            foreach (var order in (await _orderService.GetOrdersWithNavProps()).OrderByDescending(o => o.Datum))
            {
                Ordrar.Add(order);
            }

            _allaReklamationer = await _reklamationService.GetReklamationerMedDetaljer();
            UppdateraNyckeltal();
            FiltreraReklamationer();
        }

        private void UppdateraNyckeltal()
        {
            NyaAntal = _allaReklamationer.Count(r => r.Status == "Ny");
            UnderBehandlingAntal = _allaReklamationer.Count(r => r.Status == "Under behandling");
            GodkandaAntal = _allaReklamationer.Count(r => r.Status == "Godkänd");
            AtgardadeAntal = _allaReklamationer.Count(r => r.Status == "Åtgärdad");
        }

        private void FiltreraReklamationer()
        {
            Reklamationer.Clear();

            var sok = SokText?.Trim().ToLower() ?? string.Empty;
            var resultat = _allaReklamationer.AsEnumerable();

            if (ValdFilterStatus != "Alla")
            {
                resultat = resultat.Where(r => r.Status == ValdFilterStatus);
            }

            if (!string.IsNullOrWhiteSpace(sok))
            {
                resultat = resultat.Where(r =>
                    r.ReklamationID.ToString().Contains(sok) ||
                    r.OrderID.ToString().Contains(sok) ||
                    (r.Kund?.Namn ?? string.Empty).ToLower().Contains(sok) ||
                    (r.Produkt?.Namn ?? string.Empty).ToLower().Contains(sok) ||
                    (r.Orsak ?? string.Empty).ToLower().Contains(sok));
            }

            foreach (var reklamation in resultat.OrderByDescending(r => r.SkapadDatum))
            {
                Reklamationer.Add(reklamation);
            }
        }

        [RelayCommand]
        private async Task SparaReklamation()
        {
            if (ValdOrder == null)
            {
                MessageBox.Show("Välj vilken order reklamationen gäller.", "Order saknas", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Beskrivning) || Beskrivning.Trim().Length < 5)
            {
                MessageBox.Show("Beskriv reklamationen med minst 5 tecken.", "Beskrivning saknas", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ValdReklamation == null)
            {
                var reklamation = new Reklamation
                {
                    OrderID = ValdOrder.OrderID,
                    KundID = ValdOrder.KundID,
                    ProduktID = ValdProdukt?.ProduktID,
                    Orsak = ValdOrsak,
                    Beskrivning = Beskrivning.Trim(),
                    Status = ValdStatus,
                    Atgard = ValdAtgard,
                    SkapadDatum = DateTime.Now,
                    AvslutadDatum = ValdStatus == "Åtgärdad" ? DateTime.Now : null,
                    SkapadAvID = Session.CurrentUser?.AnvändarID ?? ValdOrder.StartadAvID
                };

                await _reklamationService.AddReklamation(reklamation);
            }
            else
            {
                ValdReklamation.OrderID = ValdOrder.OrderID;
                ValdReklamation.KundID = ValdOrder.KundID;
                ValdReklamation.ProduktID = ValdProdukt?.ProduktID;
                ValdReklamation.Orsak = ValdOrsak;
                ValdReklamation.Beskrivning = Beskrivning.Trim();
                ValdReklamation.Status = ValdStatus;
                ValdReklamation.Atgard = ValdAtgard;
                ValdReklamation.AvslutadDatum = ValdStatus == "Åtgärdad" ? DateTime.Now : null;

                await _reklamationService.UpdateReklamation(ValdReklamation);
            }

            await LaddaData();
            RensaFormular();
            MessageBox.Show("Reklamationen är sparad.", "Klar", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        private void SattStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return;
            }

            ValdStatus = status;
        }

        [RelayCommand]
        private async Task MarkeraAtgardad()
        {
            if (ValdReklamation == null)
            {
                MessageBox.Show("Välj en reklamation först.", "Ingen reklamation vald", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ValdReklamation.Status = "Åtgärdad";
            ValdReklamation.AvslutadDatum = DateTime.Now;
            await _reklamationService.UpdateReklamation(ValdReklamation);
            await LaddaData();
            RensaFormular();
        }

        [RelayCommand]
        private async Task TaBortReklamation()
        {
            if (ValdReklamation == null)
            {
                MessageBox.Show("Välj en reklamation att ta bort.", "Ingen reklamation vald", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var svar = MessageBox.Show($"Vill du ta bort reklamation #{ValdReklamation.ReklamationID}?", "Ta bort reklamation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (svar != MessageBoxResult.Yes)
            {
                return;
            }

            await _reklamationService.DeleteReklamation(ValdReklamation.ReklamationID);
            await LaddaData();
            RensaFormular();
        }

        [RelayCommand]
        private void RensaFormular()
        {
            ValdReklamation = null;
            ValdOrder = null;
            ValdProdukt = null;
            ValdOrsak = "Fel storlek";
            Beskrivning = string.Empty;
            ValdStatus = "Ny";
            ValdAtgard = "Reparation";
            KundText = string.Empty;
            OrderDetaljText = string.Empty;
            ProdukterIOrder.Clear();
        }
    }
}
