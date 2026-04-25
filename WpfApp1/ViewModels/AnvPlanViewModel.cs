using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using WpfApp1.Views1;

namespace WpfApp1.ViewModels
{
    public partial class AnvPlanViewModel : ObservableObject
    {
        private readonly IPlaneringsYtaService _service;
        private readonly IAktivitetService _aktivitetService;
        private readonly IAnvändarService _användarService;
        private Användare user => Session.CurrentUser; // När vm skapas, spara in den inloggade användaren från session i lokal variabel

        public event Action? RequestClosePopup;

        [ObservableProperty]
        private string valtSchemaLäge;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DennaVeckaTxt))]
        private DateTime nuvarandeMåndag;

        [ObservableProperty]
        private bool månadsvySynlig;

        [ObservableProperty]
        private DateTime? valtDatum;

        [ObservableProperty]
        private SchemaBlock valdAktivitet;

        private ObservableCollection<SchemaBlock> schema;
        public ObservableCollection<SchemaBlock> Schema
        {
            get => schema;
            set => SetProperty(ref schema, value);
        }

        public string DennaVeckaTxt => $"Vecka {ISOWeek.GetWeekOfYear(NuvarandeMåndag)}, {NuvarandeMåndag:MMMM yyyy}";

        public AnvPlanViewModel(IPlaneringsYtaService service, IAktivitetService aktivitetService, IAnvändarService användarService)
        {
            _service = service;
            _aktivitetService = aktivitetService;
            _användarService = användarService;

            DateTime idag = DateTime.Today;
            int diff = (7 + (idag.DayOfWeek - DayOfWeek.Monday)) % 7;
            NuvarandeMåndag = idag.AddDays(-1 * diff).Date;
            ValtSchemaLäge = "Mitt schema";
            _ = LaddaSchema();
        }

        partial void OnValtSchemaLägeChanged(string value)
        {
            _ = LaddaSchema();
        }

        public async Task LaddaSchema()
        {
            var veckaStart = NuvarandeMåndag;
            var veckaSlut = veckaStart.AddDays(7);

            // Hämta data från databasen i bakgrunden
            var planeringar = await _service.HämtaAllaPlaneringar(veckaStart, veckaSlut);
            var aktiviteter = await _aktivitetService.HämtaAllaAktiviteter();

            // Om användaren vill se eget schema, filtrera på användarens id
            if (ValtSchemaLäge == "Mitt schema")
            {
                planeringar = planeringar.Where(p => p.AnvändarID == Session.CurrentUser.AnvändarID)
                    .ToList();
                aktiviteter = aktiviteter.Where(a => 
                    a.SkapadAvID == Session.CurrentUser.AnvändarID ||
                    a.Deltagare.Any(d => d.AnvändarID == Session.CurrentUser.AnvändarID)
                    ).ToList();
            }

            // Skapa en helt NY, temporär lista i minnet för att inte bråka med UI-tråden
            var nyLista = new ObservableCollection<SchemaBlock>();

            foreach (var p in planeringar)
            {
                nyLista.Add(new SchemaBlock
                {
                    Id = p.PlaneringsID,
                    Typ = "Planering",
                    Titel = p.PlaneringsNamn,
                    StartTid = p.StartTid,
                    SlutTid = p.SlutTid,
                    Färg = GetFärg(p.Status),
                    OrderId = p.OrderRad?.OrderID,
                    ProduktId = p.OrderRad?.ProduktID,
                    AnvändarNamn = p.Användare?.Namn,
                    AnvändarId = p.AnvändarID,
                    ProduktNamn = p.OrderRad?.Produkt?.Namn,
                    ÄrHeldag = p.StartTid.Date != p.SlutTid.Date,
                });
            }

            foreach (var a in aktiviteter)
            {
                bool ärDeltagare = a.Deltagare.Any(u => u.AnvändarID == Session.CurrentUser.AnvändarID);
                bool ärSkapare = a.SkapadAvID == Session.CurrentUser.AnvändarID;

                nyLista.Add(new SchemaBlock
                {
                    Id = a.AktivitetID,
                    Typ = "Aktivitet",
                    Titel = a.Namn,
                    StartTid = a.StartTid,
                    SlutTid = a.SlutTid,
                    AnvändarNamn = a.SkapadAv?.Namn,
                    AnvändarId = a.SkapadAvID,
                    Färg = ärSkapare
                        ? (Brush)new BrushConverter().ConvertFrom("#8A2BE2") // din aktivitet
                        : ärDeltagare
                            ?(Brush)new BrushConverter().ConvertFrom("#FFD700") // du är deltagare
                            : (Brush)new BrushConverter().ConvertFrom("#87CEFA"),
                    ÄrHeldag = a.StartTid.Date != a.SlutTid.Date,
                    DeltagareNamn = a.Deltagare?
                        .Select(d => d.Namn)
                        .ToList() ?? new List<string>()
                });
            }
            // NÄR DEN ÄR HELT KLAR: Tilldela den till UI-variabeln i ett enda svep.
            Application.Current.Dispatcher.Invoke(() =>
            {
                Schema = nyLista;
            });
        }

        private Brush GetFärg(string status)
        {
            string färgKod = status switch
            {
                "Ej påbörjat" => "#777777",
                "Påbörjat" => "#FFA500",
                "Tillverkas" => "#1E90FF",
                "Klar för leverans" => "#00C853",
                _ => "#CCCCCC"
            };
            // Konverterar textsträngen till en riktig WPF-målarpensel
            return (Brush)new BrushConverter().ConvertFrom(färgKod);
        }

        [RelayCommand]
        private async Task NästaVecka()
        {
            NuvarandeMåndag = NuvarandeMåndag.AddDays(7);
            await LaddaSchema();
        }

        [RelayCommand]
        private async Task TidigareVecka()
        {
            NuvarandeMåndag = NuvarandeMåndag.AddDays(-7);
            await LaddaSchema();
        }

        [RelayCommand]
        private void VisaMånadsVy()
        {
            MånadsvySynlig = true;
        }

        [RelayCommand]
        private void StängMånadsvy()
        {
            MånadsvySynlig = false;
        }

        partial void OnValtDatumChanged(DateTime? value)
        {
            if (value.HasValue)
            {
                int diff = (7 + (value.Value.DayOfWeek - DayOfWeek.Monday)) % 7;
                NuvarandeMåndag = value.Value.AddDays(-1 * diff).Date;

                OnPropertyChanged(nameof(DennaVeckaTxt));
                MånadsvySynlig = false;

                _ = LaddaSchema();
            }
        }
        public bool KanTaBortVald =>
                ValdAktivitet != null &&
                ValdAktivitet.AnvändarId == Session.CurrentUser.AnvändarID;
        partial void OnValdAktivitetChanged(SchemaBlock value)
        {
            OnPropertyChanged(nameof(KanTaBortVald));
        }
        [RelayCommand]
        private async Task TaBortVald()
        {
            if (ValdAktivitet == null) return;

            if (ValdAktivitet.Typ == "Planering")
            {
                await _service.TaBortPlanering(ValdAktivitet.Id);
            }
            else if (ValdAktivitet.Typ == "Aktivitet")
            {
                await _aktivitetService.TaBortAktivitet(ValdAktivitet.Id);
            }
            ValdAktivitet = null;
            await LaddaSchema();
            RequestClosePopup?.Invoke();
        }

        [RelayCommand]
        private void ÖppnaLäggTillAktivitet()
        {
            var serviceProvider = ((App)Application.Current).ServiceProvider;

            var vm = serviceProvider.GetRequiredService<LäggTillAktivitetViewModel>();

            // sätt user manuellt
            vm.SetUser(user);

            var window = new LäggTillAktivitetWindow
            {
                DataContext = vm
            };

            window.ShowDialog();

            _ = LaddaSchema();
        }

        public async Task UppdateraTid(int id, string typ, DateTime nyStart, DateTime nySlut)
        {
            // Beroende på om det är en Planering (Hatt) eller Aktivitet (Möte) sparar vi på olika ställen
            if (typ == "Planering")
            {
                var planering = await _service.HämtaPlaneringById(id);
                if (planering != null)
                {
                    planering.StartTid = nyStart;
                    planering.SlutTid = nySlut;

                    await _service.UpdateraPlanering(planering);
                }
            }
            else if (typ == "Aktivitet")
            {
                var aktivitet = await _aktivitetService.HämtaAktivitetById(id);
                if (aktivitet != null)
                {
                    aktivitet.StartTid = nyStart;
                    aktivitet.SlutTid = nySlut;
                    await _aktivitetService.UpdateraAktivitet(aktivitet);
                }
            }

            // Ladda om kalendern så att allt ligger helt perfekt synkat med databasen
            await LaddaSchema();
        }
    }
}