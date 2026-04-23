using BL.Interfaces;
using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Input;
using Models;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Views1;

namespace WpfApp1.ViewModels
{
    public partial class LäggTillAktivitetViewModel : ObservableObject
    {
        private readonly IAktivitetService _service;
        private readonly Användare _user;
        public ObservableCollection<Användare> AllaAnvändare { get; } = new();

        public ObservableCollection<Användare> ValdaDeltagare { get; } = new();

        public LäggTillAktivitetViewModel(IAktivitetService service, Användare user)
        {
            _service = service;
            _user = user;
        }

        [ObservableProperty]
        private string titel;

        [ObservableProperty]
        private DateTime startDatum = DateTime.Now;

        [ObservableProperty]
        private TimeSpan startTid;

        [ObservableProperty]
        private TimeSpan slutTid;

        [RelayCommand]
        private async Task SparaAktivitet()
        {
            var start = startDatum.Date + startTid;
            var slut = startDatum.Date + slutTid;

            var aktivitet = new Aktivitet
            {
                Namn = Titel,
                StartTid = start,
                SlutTid = slut,
                SkapadAvID = _user.AnvändarID
            };
            aktivitet.Deltagare = ValdaDeltagare.ToList();

            await _service.LäggTillAktivitet(aktivitet);

            Application.Current.Windows
        .OfType<Window>()
        .FirstOrDefault(w => w is LäggTillAktivitetWindow)?
        .Close();
        }

    }
}
