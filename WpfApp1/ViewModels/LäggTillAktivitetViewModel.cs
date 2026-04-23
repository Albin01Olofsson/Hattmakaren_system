using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;
using System.Windows;
using WpfApp1.Views1;

namespace WpfApp1.ViewModels
{
    public partial class LäggTillAktivitetViewModel : ObservableObject
    {
        private readonly IAktivitetService _service;
        private readonly Användare _user;
        private readonly IAnvändarService _användarService;
        public ObservableCollection<Användare> AllaAnvändare { get; } = new();

        public ObservableCollection<Användare> ValdaDeltagare { get; } = new();
        public void UppdateraValdaDeltagare(System.Collections.IList selectedItems)
        {
            ValdaDeltagare.Clear();

            foreach (Användare u in selectedItems)
            {
                ValdaDeltagare.Add(u);
            }
        }

        public ObservableCollection<int> ValdaDeltagareIds { get; } = new();

        public bool ÄrAdmin => _user.IsAdmin;

        public LäggTillAktivitetViewModel(IAktivitetService service, IAnvändarService användarService, Användare user)
        {
            _service = service;
            _user = user;
            _användarService = användarService;
            _ = LaddaAnvändare();
        }
        private async Task LaddaAnvändare()
        {
            var users = await _användarService.HämtaAllaAnvändare();

            AllaAnvändare.Clear();

            foreach (var u in users)
                AllaAnvändare.Add(u);
        }
        [ObservableProperty]
        private string titel;

        [ObservableProperty]
        private DateTime startDatum = DateTime.Now;

        [ObservableProperty]
        private int? startTid;

        [ObservableProperty]
        private int? slutTid;

        [RelayCommand]
        private async Task SparaAktivitet()
        {
            var start = StartDatum.Date.AddHours(StartTid.Value);
            var slut = StartDatum.Date.AddHours(SlutTid.Value);

            var deltagare = ValdaDeltagare.Any()
                ? ValdaDeltagare.ToList()
                : new List<Användare> { _user };

            var aktivitet = new Aktivitet
            {
                Namn = Titel,
                StartTid = start,
                SlutTid = slut,
                SkapadAvID = _user.AnvändarID,
                Deltagare = deltagare
            };

            await _service.LäggTillAktivitet(aktivitet);

            Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w is LäggTillAktivitetWindow)?
                .Close();
        }

    }
}
