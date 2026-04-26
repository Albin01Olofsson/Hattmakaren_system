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
        private readonly IAnvändarService _användarService;
        private Användare _user;
        public void SetUser(Användare user)
        {
            _user = user;
        }
        public bool ÄrAdmin => _user?.IsAdmin == true;
        public ObservableCollection<SelectableAnvändare> AllaAnvändare { get; } = new();
        [ObservableProperty] private string sökText;
        public ObservableCollection<SelectableAnvändare> FiltreradeAnvändare { get; set; } = new();
        public ObservableCollection<Användare> ValdaDeltagare { get; } = new();

        [ObservableProperty] private string titel;

        [ObservableProperty] private DateTime? startDatum = DateTime.Now;

        [ObservableProperty] private int? startTid;
        [ObservableProperty] private int? startMinut;

        [ObservableProperty] private int? slutTid;
        [ObservableProperty] private int? slutMinut;

        [ObservableProperty] private DateTime? slutDatum = DateTime.Now;

        [ObservableProperty] private string namnFel;
        [ObservableProperty] private string startTidFel;
        [ObservableProperty] private string slutTidFel;
        [ObservableProperty] private string timmarFel;

        public LäggTillAktivitetViewModel(IAktivitetService service, IAnvändarService användarService)
        {
            _service = service;
            _användarService = användarService;
            _ = LaddaAnvändare();
        }

        [RelayCommand]
        private async Task SparaAktivitet()
        {
            NamnFel = "";
            StartTidFel = "";
            SlutTidFel = "";
            TimmarFel = "";
            bool HasErrors = false;
            try
            {
                if (_user == null)
                {
                    MessageBox.Show("Systemfel: Ingen användare inloggad.");
                    HasErrors = true;
                    return;
                }

                if (string.IsNullOrWhiteSpace(Titel))
                {
                    NamnFel = "Vänligen fyll i ett namn på aktiviteten.";
                    HasErrors = true;
                }
                if (!StartDatum.HasValue || !StartTid.HasValue)
                {
                    StartTidFel = "Välj datum och tid!";
                    HasErrors = true;
                }
                if (!SlutDatum.HasValue || !SlutTid.HasValue)
                {
                    SlutTidFel = "Välj datum och tid!";
                    HasErrors = true;
                }

                var start = StartDatum.Value.Date.AddHours(StartTid.Value).AddMinutes(StartMinut ?? 0);

                var slut = SlutDatum.Value.Date.AddHours(SlutTid.Value).AddMinutes(SlutMinut ?? 0);
                
                if (slut <= start)
                {
                    TimmarFel = "Sluttiden måste vara senare än starttiden!";
                    HasErrors = true;
                }
                if (HasErrors)
                    return;

                var aktivitet = new Aktivitet
                {
                    Namn = Titel,
                    StartTid = start,
                    SlutTid = slut,
                    SkapadAvID = _user.AnvändarID,
                    Deltagare = new List<Användare>()
                };
                var deltagareIds = AllaAnvändare
                    .Where(x => x.IsSelected)
                    .Select(x => x.User.AnvändarID)
                    .ToList();

                foreach (var id in deltagareIds)
                {
                    var user = await _användarService.HämtaAnvändareMedId(id);

                    if (user != null)
                    {
                        aktivitet.Deltagare.Add(user);
                    }
                }

                await _service.LäggTillAktivitet(aktivitet);

                //MessageBox.Show(aktivitet.Deltagare.Count.ToString());
                NamnFel = "";
                StartTidFel = "";
                SlutTidFel = "";
                TimmarFel = "";

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

        private async Task LaddaAnvändare()
        {
            var users = await _användarService.HämtaAllaAnvändare();

            Application.Current.Dispatcher.Invoke(() =>
            {
                AllaAnvändare.Clear();

                foreach (var u in users)
                {
                    AllaAnvändare.Add(new SelectableAnvändare
                    {
                        User = u
                    });
                }
                FiltreraAnvändare();
            });
        }
        partial void OnSökTextChanged(string value)
        {
            FiltreraAnvändare();
        }

        private void FiltreraAnvändare()
        {
            FiltreradeAnvändare.Clear();

            var filtrerade = string.IsNullOrWhiteSpace(SökText)
                ? AllaAnvändare
                : AllaAnvändare
                    .Where(a => a.User.Namn
                        .Contains(SökText, StringComparison.OrdinalIgnoreCase));

            foreach (var user in filtrerade)
            {
                FiltreradeAnvändare.Add(user);
            }
        }
    }
}