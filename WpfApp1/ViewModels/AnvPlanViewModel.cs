using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Views1.ViewModels;

namespace WpfApp1.ViewModels
{
    public partial class AnvPlanViewModel : ObservableObject
    {
        private readonly IPlaneringsYtaService _service;
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
        private ObservableCollection<Bokning> bokningar;

        public AnvPlanViewModel(IPlaneringsYtaService service)
        {
            _service = service;
            DateTime idag = DateTime.Today;
            int diff = (7 + (idag.DayOfWeek - DayOfWeek.Monday)) % 7;
            NuvarandeMåndag = idag.AddDays(-1 * diff).Date;
            LaddaTider();
            Bokningar = new ObservableCollection<Bokning>();
            LaddaBokningar();
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
            LaddaBokningar();
        }

        public void LaddaBokningar()
        {
            Bokningar.Clear();

            var veckaStart = NuvarandeMåndag.Date;
            var veckaSlut = veckaStart.AddDays(7);

            var alla = _service.HämtaAllaPlaneringar()
                .Where(p => p.StartTid >= veckaStart && p.StartTid < veckaSlut);

            if(ValtSchemaLäge == "Mitt schema")
            {
                alla = alla.Where(p => p.AnvändarID == user.AnvändarID);
            }

            var grupper = alla.GroupBy(p => p.StartTid.Date);

            foreach(var dag in grupper)
            {
                var lista = dag.OrderBy(p => p.StartTid).ToList();
                foreach(var current in lista)
                {
                    var krockar = lista.Where(p => 
                        p.StartTid < current.SlutTid && 
                        current.StartTid < p.SlutTid)
                        .ToList();

                    int index = krockar.IndexOf(current);
                    int count = krockar.Count;

                    Bokningar.Add(new Bokning
                    {
                        PlaneringsId = current.PlaneringsID,
                        AnvändarNamn = current.Användare.Namn,
                        OrderId = current.Produkt.OrderID ?? 0,
                        ProduktId = current.ProduktID,
                        ProduktNamn = current.Produkt.namn,
                        StartTid = current.StartTid,
                        LangdITimmar = (current.SlutTid - current.StartTid).TotalHours,
                        Index = index,
                        AntalIKrock = count
                    });
                }
            }
        }

        public string DennaVeckaTxt => $"Vecka {ISOWeek.GetWeekOfYear(NuvarandeMåndag)}, {NuvarandeMåndag:MMMM yyyy}";

        public string MåndagDatum => NuvarandeMåndag.ToString("dd/MM");
        public string TisdagDatum => NuvarandeMåndag.AddDays(1).ToString("dd/MM");
        public string OnsdagDatum => NuvarandeMåndag.AddDays(2).ToString("dd/MM");
        public string TorsdagDatum => NuvarandeMåndag.AddDays(3).ToString("dd/MM");
        public string FredagDatum => NuvarandeMåndag.AddDays(4).ToString("dd/MM");

        [RelayCommand]
        private void NästaVecka()
        {
            NuvarandeMåndag = NuvarandeMåndag.AddDays(7);
            LaddaBokningar();
        }

        [RelayCommand]
        private void TidigareVecka()
        {
            NuvarandeMåndag = NuvarandeMåndag.AddDays(-7);
            LaddaBokningar();
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

        [RelayCommand]
        private void DeleteBokning(int planeringsId)
        {
            _service.TaBortPlanering(planeringsId);
            LaddaBokningar();
        }
    }
}