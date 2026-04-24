using BL.Interfaces;
using BL.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1.ViewModels
{
    public partial class SkapaArtikelVM : ObservableObject
    {
        private readonly IArtikelService _artikelService;

        public SkapaArtikelVM(IArtikelService artikelService)
        {
            _artikelService = artikelService;
        }

        [ObservableProperty]
        private string namn;

        [ObservableProperty]
        private decimal pris;

        [ObservableProperty]
        private string storlek;

        [ObservableProperty]
        private string färg;

        [ObservableProperty]
        private string modell;

        [ObservableProperty]
        private string decoration;

        [ObservableProperty]
        private int antalProdukter = 1;

        [ObservableProperty]
        private string statusText;

        [RelayCommand]
        private async Task SkapaArtikel()
        {
            if (string.IsNullOrWhiteSpace(Namn))
            {
                StatusText = "Du måste ange namn.";
                return;
            }

            if (Pris <= 0)
            {
                StatusText = "Pris måste vara större än 0.";
                return;
            }

            if (AntalProdukter <= 0)
            {
                StatusText = "Antal måste vara minst 1.";
                return;
            }

            try
            {
                var artikel = new Artikel
                {
                    Namn = Namn,
                    Pris = Pris,
                    Färg = Färg,
                    Modell = Modell,
                    Decoration = Decoration
                };

                await _artikelService.SkapaArtikelMedProdukter(artikel, AntalProdukter);

                StatusText = "Artikel skapad!";

                Namn = "";
                Pris = 0;
                Storlek = "";
                Färg = "";
                Modell = "";
                Decoration = "";
                AntalProdukter = 1;
            }
            catch (Exception ex)
            {
                StatusText = "Fel: " + ex.Message;
            }
        }
    }
}