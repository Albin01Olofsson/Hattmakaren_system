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

        [ObservableProperty]
        private DateTime slutDatum = DateTime.Now;

        [RelayCommand]

        private async Task SparaAktivitet()
        {
            try
            {
                if (_user == null)
                {
                    MessageBox.Show("Systemfel: Ingen användare inloggad.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Titel))
                {
                    MessageBox.Show("Vänligen fyll i ett namn på aktiviteten.");
                    return;
                }

                var start = startDatum.Date + startTid;
                var slut = slutDatum.Date + slutTid;

                if (slut <= start)
                {
                    MessageBox.Show("Sluttiden måste vara senare än starttiden!");
                    return;
                }

                var aktivitet = new Aktivitet
                {
                    Namn = Titel,
                    StartTid = start,
                    SlutTid = slut,
                    SkapadAvID = _user.AnvändarID
                };

                aktivitet.Deltagare = ValdaDeltagare.ToList();
                await _service.LäggTillAktivitet(aktivitet);

                // Stäng fönstret
                Application.Current.Windows
                    .OfType<Window>()
                    .FirstOrDefault(w => w is LäggTillAktivitetWindow)?
                    .Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Ett fel uppstod: {ex.Message}\n\nInre fel: {ex.InnerException?.Message}");
            }
        }
    }

}