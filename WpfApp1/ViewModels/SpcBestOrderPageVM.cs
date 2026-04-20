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
using System.Windows.Media.Imaging;

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
        private string nyttProduktNamn = String.Empty;

        [ObservableProperty]
        private decimal nyttPris;

        [ObservableProperty]
        private string nyStorlek;

        [ObservableProperty]
        private string nyBeskrivning = String.Empty;

        [ObservableProperty]
        private string bildUrl = String.Empty;

        [ObservableProperty]
        private BitmapImage? valdBild; //Bara för förhandvisning efter att man valt bild

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
            //Validering - Start

            //Produkt namn
            if (ValdProdukt != null && !string.IsNullOrWhiteSpace(NyttProduktNamn)) //Om Man valt ett produktnamn och skrivit in ett produktnamn
            {
                MessageBox.Show("Du har valt ett produkt namn och angett ett eget produktnamn, du kan bara göra en av dem.", "Krock Produkt nammn!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttProduktNamn = string.Empty;
                ValdProdukt = null;
                return;
            }

            if(NyttProduktNamn.Length < 3)
            {
                MessageBox.Show("Det angivna produktnamnet är för kort, ange ett prduktnamn som är 3-32 tecken långt", "Produkt nammn för kort!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttProduktNamn = string.Empty;
                return;
            }else if(NyttProduktNamn.Length > 32)
            {
                MessageBox.Show("Det angivna produktnamnet är för kort, ange ett prduktnamn som är 3-32 tecken långt", "Produkt nammn för långt!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttProduktNamn = string.Empty;
                return;
            }

            //Pris
            if(NyttPris == 0)
            {
                MessageBox.Show("För lågt pris, ange pris över 0 kr", "Pris för lågt!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttPris = 0;
                return;
            }else if(NyttPris > 999999)
            {
                MessageBox.Show("För högt pris, ange pris över under 1 000 000 kr", "Pris för Högt!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttPris = 0;
                return;
            }

            //Storlek
            if (string.IsNullOrWhiteSpace(NyStorlek))
            {
                MessageBox.Show("En storlek måste vara angiven.", "Storlek ej angiven!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyStorlek = String.Empty;
                return;
            }
            else if (NyStorlek.Length > 76)
            {
                MessageBox.Show("Max antal tecken är 76", "För många tecken storlek!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyStorlek = string.Empty;
                return;
            }

            //Beskrivning
            if(NyBeskrivning.Length > 500)
            {
                MessageBox.Show("Beskrivning är för lång, max 500 tecken", "För många tecken storlek!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyBeskrivning = string.Empty;
                return;
            }

            //Validering - slut

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
            ValdBild = null;
            ValdProdukt = null;
            ValdMaterial = null;
        }
    }
}
