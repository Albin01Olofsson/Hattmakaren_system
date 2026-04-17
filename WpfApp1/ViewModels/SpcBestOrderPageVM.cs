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

namespace WpfApp1.ViewModels
{
    public partial class SpcBestOrderPageVM : ObservableObject
    {
        private IOrderService _orderService; //För skapa order
        private IProduktService _produktService; //För att hämta produkt och lägga till specialbeställning
        private IMaterialService _materialService; //Hantera material
        private IKundService _kundService; //För att veta vilken kund som ska ha ordern
        private IAnvändarService _användarService; //(Ej säker på om den behövs)För att veta vem som ska ha hand om att skapa produkten, ev vem som startade ordern

        [ObservableProperty]
        private ObservableCollection<Produkt> produktLista;

        [ObservableProperty]
        private Produkt? valdProdukt;

        [ObservableProperty]
        private ObservableCollection<Material> materialLista;

        [ObservableProperty]
        private Material? valdMaterial;

        [ObservableProperty]
        private string nyttProduktNamn;

        [ObservableProperty]
        private string nyStorlek;

        [ObservableProperty]
        private string nyBeskrivning = String.Empty;

        [ObservableProperty]
        private string bildUrl = String.Empty;

        public SpcBestOrderPageVM(IOrderService orderService, IProduktService produktService, IMaterialService materialService, IKundService kundService, IAnvändarService användarService)
        {
            _orderService = orderService;
            _produktService = produktService;
            _materialService = materialService;
            _kundService = kundService;
            _användarService = användarService;

            ProduktLista = new ObservableCollection<Produkt>(_produktService.GetProdukter());
            MaterialLista = new ObservableCollection<Material>(_materialService.GetMaterialLista());
        }

        [RelayCommand]
        private void LäggTillSpecialBeställning()
        {
            Användare startadAvAnvändare = Session.CurrentUser;

            var nySpecBes = new SpecialBeställning
            {
                namn = NyttProduktNamn,
                Storlek = NyStorlek,
                Beskrivning = NyBeskrivning,
                BildURL = BildUrl,
                TillverkadAVID = startadAvAnvändare.AnvändarID
            };

            _produktService.AddSpecialBeställning(nySpecBes);
        }
    }
}
