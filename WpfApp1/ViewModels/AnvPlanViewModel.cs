using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Views1.ViewModels;

namespace WpfApp1.ViewModels
{
    public partial class AnvPlanViewModel : ObservableObject
    {
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

        public AnvPlanViewModel()
        {
            DateTime idag = DateTime.Today;
            int diff = (7 + (idag.DayOfWeek - DayOfWeek.Monday)) % 7;
            NuvarandeMåndag = idag.AddDays(-1 * diff).Date;

            LaddaTider();

            Bokningar = new ObservableCollection<Bokning>();

            Bokningar.Add(new Bokning
            {
                Titel = "Projekt Arbete",
                StartTid = NuvarandeMåndag.AddHours(8),
                LangdITimmar = 2.5
            });

        }

        public void LaddaTider()
        {
            TidsIntervaller = new ObservableCollection<string>();
            for (int i = 8; i <= 17; i++)
            {
                TidsIntervaller.Add($"{i:D2}:00");
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
        }

        [RelayCommand]
        private void TidigareVecka()
        {
            NuvarandeMåndag = NuvarandeMåndag.AddDays(-7);
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

    }
}