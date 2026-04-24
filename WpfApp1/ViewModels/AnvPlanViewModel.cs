using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private Användare user => Session.CurrentUser; // När vm skapas, spara in den inloggade användaren från session i lokal variabel

        [ObservableProperty]
        private string valtSchemaLäge;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DennaVeckaTxt))]
        private DateTime nuvarandeMåndag;

        [ObservableProperty]
        private bool månadsvySynlig;

        [ObservableProperty]
        private DateTime? valtDatum;

        private ObservableCollection<SchemaBlock> schema;
        public ObservableCollection<SchemaBlock> Schema
        {
            get => schema;
            set => SetProperty(ref schema, value);
        }

        public string DennaVeckaTxt => $"Vecka {ISOWeek.GetWeekOfYear(NuvarandeMåndag)}, {NuvarandeMåndag:MMMM yyyy}";

        public AnvPlanViewModel(IPlaneringsYtaService service, IAktivitetService aktivitetService)
        {
            _service = service;
            _aktivitetService = aktivitetService;

            DateTime idag = DateTime.Today;
            int diff = (7 + (idag.DayOfWeek - DayOfWeek.Monday)) % 7;
            NuvarandeMåndag = idag.AddDays(-1 * diff).Date;

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
                    ProduktNamn = p.OrderRad?.Produkt?.Namn
                });
            }

            foreach (var a in aktiviteter)
            {
                nyLista.Add(new SchemaBlock
                {
                    Id = a.AktivitetID,
                    Typ = "Aktivitet",
                    Titel = a.Namn,
                    StartTid = a.StartTid,
                    SlutTid = a.SlutTid,
                    Färg = (Brush)new BrushConverter().ConvertFrom("#8A2BE2"),
                    AnvändarNamn = a.SkapadAv?.Namn
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

        [RelayCommand]
        private async Task DeleteBokning(int planeringsId)
        {
            await _service.TaBortPlanering(planeringsId);
            await LaddaSchema();
        }

        [RelayCommand]
        private void ÖppnaLäggTillAktivitet()
        {
            var window = new LäggTillAktivitetWindow();

            var vm = new LäggTillAktivitetViewModel(_aktivitetService, user);
            window.DataContext = vm;

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