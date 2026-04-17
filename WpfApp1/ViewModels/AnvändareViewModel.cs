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

        public AnvändareViewModel(IAnvändarService användareService)
        {
            _användareService = användareService;
            AnvändareLista = new ObservableCollection<Användare>(
                _användareService.HämtaAllaAnvändare()
                .Where(a => a.IsActive));
        }

        [RelayCommand]
        private void Inaktivera()
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
            _användareService.InaktiveraAnvändare(ValdAnvändare.AnvändarID);
            AnvändareLista.Remove(ValdAnvändare);
        }

        public void Reload()
        {
            AnvändareLista.Clear();

            var users = _användareService.HämtaAllaAnvändare()
                .Where(a => a.IsActive);
            foreach(var u in users)
            {
                AnvändareLista.Add(u);
            }
        }
    }
}
