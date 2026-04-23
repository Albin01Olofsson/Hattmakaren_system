using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;
using System.Globalization;
using WpfApp1.Views1.ViewModels;
using Microsoft.EntityFrameworkCore;
using WpfApp1.Views1;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp1.ViewModels
{
    public partial class AnvPlanViewModel : ObservableObject
    {
        private readonly IPlaneringsYtaService _service;
        private readonly IAktivitetService _aktivitetService;
        private readonly IAnvändarService _användarService;
        private Användare user => Session.CurrentUser;//När vm skapas, spara in den inloggade användaren från session i lokal variabel

        [ObservableProperty]
        private string valtSchemaLäge;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DennaVeckaTxt))]
        [NotifyPropertyChangedFor(nameof(MåndagDatum))]
        [NotifyPropertyChangedFor(nameof(TisdagDatum))]
        [NotifyPropertyChangedFor(nameof(OnsdagDatum))]
        [NotifyPropertyChangedFor(nameof(TorsdagDatum))]
        [NotifyPropertyChangedFor(nameof(FredagDatum))]
        private DateTime nuvarandeMåndag;

        [ObservableProperty]
        private ObservableCollection<string> tidsIntervaller = new ObservableCollection<string>();

        [ObservableProperty]
        private bool månadsvySynlig;

        [ObservableProperty]
        private DateTime? valtDatum;

        [ObservableProperty]
        private ObservableCollection<SchemaBlock> schema = new();
        //private ObservableCollection<Bokning> bokningar;

        private bool _isLoading;

        public AnvPlanViewModel(IPlaneringsYtaService service, IAktivitetService aktivitetService, IAnvändarService användarService)
        {
            _service = service;
            _aktivitetService = aktivitetService;
            _användarService = användarService;

            ValtSchemaLäge = "Mitt schema";

            DateTime idag = DateTime.Today;
            int diff = (7 + (idag.DayOfWeek - DayOfWeek.Monday)) % 7;
            NuvarandeMåndag = idag.AddDays(-1 * diff).Date;
            LaddaTider();
        }

        public void LaddaTider()
        {
            TidsIntervaller = new ObservableCollection<string>();
            for (int i = 8; i <= 17; i++)
            {
                TidsIntervaller.Add($"{i:D2}:00");
            }
        }

        partial void OnValtSchemaLägeChanged(string value)
        {
            _ = LaddaSchema();
            //_ = LaddaBokningar();
        }
        
        public async Task LaddaSchema()
        {
            if (_isLoading)
                return;
            _isLoading = true;
            try
            {
                Schema.Clear();

                var veckaStart = NuvarandeMåndag;
                var veckaSlut = veckaStart.AddDays(7);

                var allaPlaneringar = await _service.HämtaAllaPlaneringar(veckaStart, veckaSlut);

                var planeringar = ValtSchemaLäge switch
                {
                    "Mitt schema" => allaPlaneringar
                        .Where(p => p.AnvändarID == user.AnvändarID)
                        .ToList(),

                    "Allas schema" => allaPlaneringar,

                    _ => allaPlaneringar
                };

                await Task.Delay(1);

                var allaAktiviteter = (await _aktivitetService.HämtaAllaAktiviteter())
                    .Where(a =>
                        a.StartTid < veckaSlut &&
                        a.SlutTid > veckaStart)
                    .ToList();

                var aktiviteter = ValtSchemaLäge switch
                {
                    "Mitt schema" => allaAktiviteter
                        .Where(a =>
                            a.SkapadAvID == user.AnvändarID ||
                            a.Deltagare.Any(d => d.AnvändarID == user.AnvändarID))
                        .ToList(),

                    "Allas schema" => user.IsAdmin
                        ? allaAktiviteter
                        : allaAktiviteter.Where(a =>
                            a.SkapadAvID == user.AnvändarID ||
                            a.Deltagare.Any(d => d.AnvändarID == user.AnvändarID))
                            .ToList(),

                    _ => allaAktiviteter
                };

                var alla = new List<(DateTime start, DateTime slut, SchemaBlock block)>();

                //void AddItem(DateTime start, DateTime slut, SchemaBlock baseBlock)
                //{
                //    double pixelsPerHour = 60; 

                //    double startHour = start.Hour + start.Minute / 60.0;
                //    double endHour = slut.Hour + slut.Minute / 60.0;

                //    double startFrom8 = startHour - 8; // eftersom din grid börjar 08:00
                //    double duration = endHour - startHour;

                //    Schema.Add(new SchemaBlock
                //    {
                //        Id = baseBlock.Id,
                //        Typ = baseBlock.Typ,
                //        Titel = baseBlock.Titel,

                //        StartTid = start,
                //        SlutTid = slut,

                //        Kolumn = ((int)start.DayOfWeek + 6) % 7,


                //        Top = (startFrom8 * pixelsPerHour)+45,
                //        Height = duration * pixelsPerHour,

                //        Färg = baseBlock.Färg,
                //        ZIndex = baseBlock.ZIndex,
                //        OrderId = baseBlock.OrderId,
                //        ProduktId = baseBlock.ProduktId,
                //        AnvändarNamn = baseBlock.AnvändarNamn,
                //        ProduktNamn = baseBlock.ProduktNamn
                //    });
                //}
                void AddItem(DateTime start, DateTime slut, SchemaBlock baseBlock)
                {
                    bool isMultiDay = start.Date != slut.Date;

                    int pixelsPerHour = 60;

                    //vilken kolumn (dag i veckan)
                    int startColumn = ((int)start.DayOfWeek + 6) % 7;
                    int endColumn = ((int)slut.DayOfWeek + 6) % 7;

                    //EN DAG (VERTIKAL)

                    if (!isMultiDay)
                    {
                        double startHour = start.Hour + start.Minute / 60.0;
                        double endHour = slut.Hour + slut.Minute / 60.0;

                        double startFrom8 = startHour - 8;
                        double duration = endHour - startHour;

                        Schema.Add(new SchemaBlock
                        {
                            Id = baseBlock.Id,
                            Typ = baseBlock.Typ,
                            Titel = baseBlock.Titel,

                            StartTid = start,
                            SlutTid = slut,

                            Kolumn = startColumn,

                            Top = startFrom8 * pixelsPerHour,
                            Height = duration * pixelsPerHour,

                            Färg = baseBlock.Färg,
                            ZIndex = baseBlock.ZIndex,
                            OrderId = baseBlock.OrderId,
                            ProduktId = baseBlock.ProduktId,
                            AnvändarNamn = baseBlock.AnvändarNamn,
                            ProduktNamn = baseBlock.ProduktNamn,
                            AnvändarId = baseBlock.AnvändarId,
                            InfoText = baseBlock.InfoText
                        });

                        return;
                    }

                    //FLERA DAGAR (HORISONTELL)

                    DateTime veckaSlut = NuvarandeMåndag.AddDays(7);
                    DateTime cursor = start;

                    while (cursor.Date <= slut.Date && cursor < veckaSlut)
                    {
                        bool isFirstDay = cursor.Date == start.Date;
                        bool isLastDay = cursor.Date == slut.Date;

                        DateTime dayStart = cursor.Date.AddHours(8);
                        DateTime dayEnd = cursor.Date.AddHours(17);

                        DateTime segmentStart =
                            isFirstDay ? start : dayStart;

                        DateTime segmentEnd =
                            isLastDay ? slut : dayEnd;

                        // clamp mot veckan, stoppar rendering utanför vecka
                        if (segmentStart > veckaSlut)
                            break;

                        if (segmentEnd > veckaSlut)
                            segmentEnd = veckaSlut;

                        Schema.Add(new SchemaBlock
                        {
                            Id = baseBlock.Id,
                            Typ = baseBlock.Typ,
                            Titel = baseBlock.Titel,

                            StartTid = segmentStart,
                            SlutTid = segmentEnd,

                            Kolumn = ((int)cursor.DayOfWeek + 6) % 7,

                            Top = isFirstDay ? (segmentStart.Hour - 8) * pixelsPerHour : 0,
                            Height = (segmentEnd - segmentStart).TotalHours * pixelsPerHour,

                            Färg = baseBlock.Färg,
                            ZIndex = baseBlock.ZIndex,
                            OrderId = baseBlock.OrderId,
                            ProduktId = baseBlock.ProduktId,
                            AnvändarNamn = baseBlock.AnvändarNamn,
                            ProduktNamn = baseBlock.ProduktNamn,
                            AnvändarId = baseBlock.AnvändarId,
                            InfoText = baseBlock.InfoText
                        });

                        cursor = cursor.AddDays(1);
                    }
                }

                foreach (var p in planeringar)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"PlaneringID: {p.PlaneringsID}, AnvändarID: {p.AnvändarID}, SkapadAv: {p.Användare?.AnvändarID}"
                    );
                }
                foreach (var p in planeringar)
                {
                    AddItem(p.StartTid, p.SlutTid, new SchemaBlock
                    {
                        Id = p.PlaneringsID,
                        Typ = "Planering",
                        Titel = p.PlaneringsNamn,
                        Färg = GetFärg(p.Status),
                        ZIndex = 1,
                        OrderId = p.OrderRad?.OrderID,
                        ProduktId = p.OrderRad?.ProduktID,
                        AnvändarNamn = p.Användare?.Namn,
                        AnvändarId = p.AnvändarID,

                        InfoText =
                            $"Planering\n" +
                            $"Produkt: {p.OrderRad?.Produkt?.Namn}\n" +
                            $"Order: {p.OrderRad?.OrderID}\n" +
                            $"Användare: {p.Användare?.Namn}\n" +
                            $"Status: {p.Status}"
                    });
                }

                foreach (var a in aktiviteter)
                {
                    AddItem(a.StartTid, a.SlutTid, new SchemaBlock
                    {
                        Id = a.AktivitetID,
                        Typ = "Aktivitet",
                        Titel = a.Namn,
                        Färg = "#8A2BE2",
                        ZIndex = 2,
                        AnvändarNamn = a.SkapadAv?.Namn,
                        AnvändarId = a.SkapadAvID,

                        InfoText =
                            $"Aktivitet\n" +
                            $"Namn: {a.Namn}\n" +
                            $"Skapad av: {a.SkapadAv?.Namn}"
                    });
                }
            }
            finally
            {
                _isLoading = false;
            }
            
        }
        private string GetFärg(string status)
        {
            return status switch
            {
                "Ej påbörjat" => "#777777",
                "Påbörjat" => "#FFA500",
                "Tillverkas" => "#1E90FF",
                "Klar för leverans" => "#00C853",
                _ => "#CCCCCC"
            };
        }
        #region
        //public async Task LaddaBokningar()
        //{
        //    Bokningar.Clear();

        //    var veckaStart = NuvarandeMåndag.Date;
        //    var veckaSlut = veckaStart.AddDays(7);

        //    var alla = await _service.HämtaAllaPlaneringar(veckaStart, veckaSlut);

        //    if(ValtSchemaLäge == "Mitt schema")
        //    {
        //        alla = alla.Where(p => p.AnvändarID == user.AnvändarID).ToList();
        //    }

        //    var grupper = alla.GroupBy(p => p.StartTid.Date);

        //    foreach(var dag in grupper)
        //    {
        //        var lista = dag.OrderBy(p => p.StartTid).ToList();
        //        foreach(var current in lista)
        //        {
        //            var krockar = lista.Where(p => 
        //                p.StartTid < current.SlutTid && 
        //                current.StartTid < p.SlutTid)
        //                .ToList();

        //            int index = krockar.IndexOf(current);
        //            int count = krockar.Count;

        //            Bokningar.Add(new Bokning
        //            {
        //                PlaneringsId = current.PlaneringsID,
        //                AnvändarNamn = current.Användare.Namn,
        //                //OrderId = current.Produkt.OrderID ?? 0,
        //                ProduktId = current.ProduktID,
        //                ProduktNamn = current.Produkt.Namn,
        //                StartTid = current.StartTid,
        //                LangdITimmar = (current.SlutTid - current.StartTid).TotalHours,
        //                Index = index,
        //                AntalIKrock = count
        //            });
        //        }
        //    }
        //}
        #endregion
        public string DennaVeckaTxt => $"Vecka {ISOWeek.GetWeekOfYear(NuvarandeMåndag)}, {NuvarandeMåndag:MMMM yyyy}";

        public string MåndagDatum => NuvarandeMåndag.ToString("dd/MM");
        public string TisdagDatum => NuvarandeMåndag.AddDays(1).ToString("dd/MM");
        public string OnsdagDatum => NuvarandeMåndag.AddDays(2).ToString("dd/MM");
        public string TorsdagDatum => NuvarandeMåndag.AddDays(3).ToString("dd/MM");
        public string FredagDatum => NuvarandeMåndag.AddDays(4).ToString("dd/MM");

        [RelayCommand]
        private async Task NästaVecka()
        {
            NuvarandeMåndag = NuvarandeMåndag.AddDays(7);
            await LaddaSchema();
            //await LaddaBokningar();
        }

        [RelayCommand]
        private async Task TidigareVecka()
        {
            NuvarandeMåndag = NuvarandeMåndag.AddDays(-7);
            //await LaddaBokningar();
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
            }
        }

        //[RelayCommand]
        //private async Task DeleteBokning(int planeringsId)
        //{
        //    await _service.TaBortPlanering(planeringsId);
        //    //await LaddaBokningar();
        //    await LaddaSchema();
        //}
        [RelayCommand]
        private async Task DeleteItem(SchemaBlock block)
        {
            if (block == null)
                return;

            
            if (block.Typ == "Planering")
            {
                await _service.TaBortPlanering(block.Id);
            }
            else if (block.Typ == "Aktivitet")
            {
                await _aktivitetService.TaBortAktivitet(block.Id);
            }

            await LaddaSchema();
        }
        
        [RelayCommand]
        private void ÖppnaLäggTillAktivitet()
        {
            var window = new LäggTillAktivitetWindow();

            var vm = new LäggTillAktivitetViewModel(_aktivitetService, _användarService, user);
            window.DataContext = vm;

            window.ShowDialog();

            _ = LaddaSchema(); 
        }
    }
}