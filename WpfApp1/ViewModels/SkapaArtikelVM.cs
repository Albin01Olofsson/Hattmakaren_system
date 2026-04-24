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
        private IOrderService _orderService; //För skapa order
        private IProduktService _produktService; //För att hämta produkt och lägga till specialbeställning
        private IMaterialService _materialService; //Hantera material
        private IKundService _kundService; //För att veta vilken kund som ska ha ordern
        private IAnvändarService _användarService; //(Ej säker på om den behövs)För att veta vem som ska ha hand om att skapa produkten, ev vem som startade ordern
        private IArtikelService _artikelService;
        public SkapaArtikelVM(IArtikelService artikelService, IOrderService orderService, IProduktService produktService, IMaterialService materialService, IKundService kundService, IAnvändarService användarService)
        {
            _orderService = orderService;
            _produktService = produktService;
            _materialService = materialService;
            _kundService = kundService;
            _användarService = användarService;
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
                    Storlek = Storlek,
                    Färg = Färg,
                    Modell = Modell,
                    Decoration = Decoration
                };

                await _artikelService.SkapaArtikelMedProdukter(artikel, AntalProdukter);

                StatusText = "✅ Artikel skapad!";

                // reset
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
                StatusText = "❌ Fel: " + ex.Message;
            }
        }





    }
}
