using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;

namespace WpfApp1.ViewModels
{
    public partial class KundVM : ObservableObject
    {
        private readonly IKundService _kundService;
        private List<Kund> _allaKunder = new();

        public ObservableCollection<Kund> Kunder { get; } = new();


        [ObservableProperty]
        private string _sökText;

        partial void OnSökTextChanged(string value)
        {
            FiltreraKunder();
        }

        public KundVM(IKundService kundService)
        {
            _kundService = kundService;
            _ = LaddaData();
        }

        private async Task LaddaData()
        {
            _allaKunder = await _kundService.HämtaAllaKunder();
            FiltreraKunder();
        }

        private void FiltreraKunder()
        {
            Kunder.Clear();
            var resultat = _allaKunder
                .Where(k => k.Namn != "Borttagen kund") // Dölj de anonymiserade
                .Where(k => string.IsNullOrEmpty(SökText) ||
                            k.Namn.ToLower().Contains(SökText.ToLower()));

            foreach (var kund in resultat)
            {
                Kunder.Add(kund);
            }
        }

        [RelayCommand]
        private async Task TaBortKund(Kund kund)
        {
            // 1. Kör din anonymisering
            await _kundService.AnonymiseraKund(kund.KundID);

            // 2. Uppdatera listan så kunden försvinner direkt i UI
            await LaddaData();
        }
    }
}
