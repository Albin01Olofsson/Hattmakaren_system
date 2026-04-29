using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class AnvändareViewModel : ObservableObject
    {
        private readonly IAnvändarService _användareService;

        public ObservableCollection<Användare> AnvändareLista { get; set; }

        [ObservableProperty]
        private Användare valdAnvändare;
        [ObservableProperty]
        private bool visaAnvändarPopup;
        public AnvändareViewModel(IAnvändarService användareService)
        {
            _användareService = användareService;
            AnvändareLista = new ObservableCollection<Användare>();
            _ = Ladda();
        }

        public async Task Ladda()
        {
            var users = await _användareService.HämtaAllaAnvändare();
            AnvändareLista.Clear();
            foreach (var u in users)
            {
                AnvändareLista.Add(u);
            }
        }

        [RelayCommand]
        private async Task Inaktivera()
        {
            if(ValdAnvändare == null)
            {
                MessageBox.Show("Välj en användare först!");
                return;
            }

            if (Session.CurrentUser.IsAdmin == false)
            {
                MessageBox.Show("Du har inte behörighet att inaktivera användare!");
                return;
            }

            if (ValdAnvändare.AnvändarID == Session.CurrentUser.AnvändarID)
            {
                MessageBox.Show("Du kan inte inaktivera ditt eget konto!");
                return;
            }

            var resultat = MessageBox.Show("Är du säker?", "Bekräfta", MessageBoxButton.YesNo);
            if(resultat != MessageBoxResult.Yes) 
            {
                return;
            }
            await _användareService.InaktiveraAnvändare(ValdAnvändare.AnvändarID);
            AnvändareLista.Remove(ValdAnvändare);
        }

        public async Task Reload()
        {
            AnvändareLista.Clear();

            var users = await _användareService.HämtaAllaAnvändare();
            foreach(var u in users)
            {
                AnvändareLista.Add(u);
            }
        }
        partial void OnValdAnvändareChanged(Användare value)
        {
            if (value != null)
            {
                VisaAnvändarPopup = true;
            }
        }
        [RelayCommand]
        private void StängPopup()
        {
            VisaAnvändarPopup = false;
        }
    }
}
