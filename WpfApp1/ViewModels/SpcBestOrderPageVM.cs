using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private decimal nyttPris;

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

            string tillagdBildPath = string.Empty;

            var nySpecBes = new SpecialBeställning
            {
                namn = NyttProduktNamn,
                pris = NyttPris,
                Storlek = NyStorlek,
                Beskrivning = NyBeskrivning,
                TillverkadAVID = startadAvAnvändare.AnvändarID
            };

            if (!string.IsNullOrWhiteSpace(BildUrl))
            {
                string extension = Path.GetExtension(BildUrl);
                string bildNamn = $"SpecialBest-{nySpecBes.namn}-{Guid.NewGuid()}{extension}";

                string baseDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName; //Sökväg för lägsta nivån i projektet, stega bakåt till projektets lägsta nivå
                string bildMapp = System.IO.Path.Combine(baseDirectory, "DAL", "Bilder"); //Från projektets basmapp, gå in i DAL, från DAL gå in I Bilder
                string bildFullPath = System.IO.Path.Combine(bildMapp, bildNamn); //Slå ihop bildmappens sökväg med Bildens filnamn.
                Directory.CreateDirectory(bildMapp); //Tvinga behövd mappstruktur att skapas om det har flyttats eller raderats något
                File.Copy(BildUrl, bildFullPath, true);
                tillagdBildPath = Path.Combine("Bilder", bildNamn);
            }

            nySpecBes.BildURL = tillagdBildPath; //Tilldela "Bilder/filnamn" som sökväg till specialbeställnings objektet

            _produktService.AddSpecialBeställning(nySpecBes);

            MessageBox.Show("Sparad!", "Klar", MessageBoxButton.OK, MessageBoxImage.Information);

            NyttProduktNamn = string.Empty;
            NyttPris = 0;
            NyStorlek = string.Empty;
            NyBeskrivning = string.Empty;
            BildUrl = string.Empty;
            ValdProdukt = null;
        }
    }
}
