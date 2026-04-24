using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp1.ViewModels
{
    public partial class SkapaLagerfördProduktVM : ObservableObject
    {
        private IOrderService _orderService;
        private IProduktService _produktService;
        private IMaterialService _materialService;
        private IKundService _kundService;
        private IAnvändarService _användarService;

        [ObservableProperty]
        private ObservableCollection<Material> materialLista;

        [ObservableProperty]
        private ObservableCollection<Material> nyMaterialLista;

        [ObservableProperty]
        private Material? valdMaterial;

        [ObservableProperty]
        private string nyttProduktNamn = String.Empty;

        [ObservableProperty]
        private decimal nyttPris;

        [ObservableProperty]
        private int nyttLagerantal; // TILLAGD FÖR LAGERSALDO

        [ObservableProperty]
        private string nyStorlek;

        [ObservableProperty]
        private string nyTyp;

        [ObservableProperty]
        private string nyModell;

        [ObservableProperty]
        private string nyFärg;

        [ObservableProperty]
        private string? nyDecoration = string.Empty;

        [ObservableProperty]
        private string nyBeskrivning = String.Empty;

        [ObservableProperty]
        private int artikelId;

        public SkapaLagerfördProduktVM(IOrderService orderService, IProduktService produktService, IMaterialService materialService, IKundService kundService, IAnvändarService användarService)
        {
            _orderService = orderService;
            _produktService = produktService;
            _materialService = materialService;
            _kundService = kundService;
            _användarService = användarService;

            MaterialLista = new ObservableCollection<Material>();
            NyMaterialLista = new ObservableCollection<Material>();
            LaddaData();
        }

        private async Task LaddaData()
        {
            var material = await _materialService.GetMaterialLista();

            foreach (var m in material)
                MaterialLista.Add(m);
        }

        [RelayCommand]
        private void LäggTillMaterial()
        {
            if (ValdMaterial == null)
                return;

            bool redanTillagd = NyMaterialLista.Any(m => m.Namn == ValdMaterial.Namn);

            if (!redanTillagd)
            {
                NyMaterialLista.Add(ValdMaterial);
            }
        }

        [RelayCommand]
        private async Task LaggTillLagerfordProdukt()
        {
            // Validering Namn
            if (NyttProduktNamn.Length < 3)
            {
                MessageBox.Show("Det angivna produktnamnet är för kort, ange ett prduktnamn som är 3-32 tecken långt", "Produktnamn för kort!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttProduktNamn = string.Empty;
                return;
            }
            else if (NyttProduktNamn.Length > 32)
            {
                MessageBox.Show("Det angivna produktnamnet är för långt, ange ett prduktnamn som är 3-32 tecken långt", "Produktnamn för långt!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttProduktNamn = string.Empty;
                return;
            }

            // Pris
            if (NyttPris == 0)
            {
                MessageBox.Show("För lågt pris, ange pris över 0 kr", "Pris för lågt!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttPris = 0;
                return;
            }
            else if (NyttPris > 999999)
            {
                MessageBox.Show("För högt pris, ange pris under 1 000 000 kr", "Pris för högt!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttPris = 0;
                return;
            }

            // Validering Lagersaldo (Ny!)
            if (NyttLagerantal < 0)
            {
                MessageBox.Show("Lagersaldot kan inte vara negativt.", "Felaktigt saldo", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyttLagerantal = 0;
                return;
            }

            // Storlek
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

            // Typ
            if (string.IsNullOrWhiteSpace(NyTyp))
            {
                MessageBox.Show("En typ för hatten måste anges.", "Typ ej angiven!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyTyp = String.Empty;
                return;
            }
            else if (NyTyp.Length < 3 || NyTyp.Length > 76)
            {
                MessageBox.Show("Teckenantalet angivet för 'typ' måste vara 3-76 tecken", "Problem med antal tecken!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyTyp = string.Empty;
                return;
            }

            // Modell
            if (string.IsNullOrWhiteSpace(NyModell))
            {
                MessageBox.Show("En Modell för hatten måste anges.", "Modell ej angiven!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyModell = String.Empty;
                return;
            }
            else if (NyModell.Length < 3 || NyModell.Length > 76)
            {
                MessageBox.Show("Teckenantalet angivet för 'Modell' måste vara 3-76 tecken", "För många tecken Modell!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyModell = string.Empty;
                return;
            }

            // Färg
            if (string.IsNullOrWhiteSpace(NyFärg))
            {
                MessageBox.Show("Färg för hatten måste anges.", "Färg ej angiven!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyFärg = String.Empty;
                return;
            }
            else if (NyFärg.Length < 3 || NyFärg.Length > 76)
            {
                MessageBox.Show("Teckenantalet angivet för 'Färg' måste vara 3-76 tecken", "För många tecken Färg!", MessageBoxButton.OK, MessageBoxImage.Warning);
                NyFärg = string.Empty;
                return;
            }

            Användare startadAvAnvändare = Session.CurrentUser;

            
            LagerfördProdukt nyProd = new LagerfördProdukt
            {
                Namn = NyttProduktNamn,
                Pris = NyttPris,
                Storlek = NyStorlek,
                HattTyp = NyTyp,
                Modell = NyModell,
                Färg = NyFärg,
                Decoration = NyDecoration,
                Lagerantal = NyttLagerantal,
                TillverkadAVID = startadAvAnvändare.AnvändarID
            };
            var artikelLista = new List<Produkt>();
            artikelLista.Add(nyProd);
            Artikel artikel = new Artikel
            {
                ArtikelId = artikelId,
                Namn = NyttProduktNamn,
                Pris = NyttPris,
                HattTyp = NyTyp,
                Modell = NyModell,
                Färg = NyFärg,
                Decoration = NyDecoration,
                Antal = NyttLagerantal,
                Produkter =artikelLista
            };

            List<int> materialIds = NyMaterialLista.Select(m => m.MaterialID).ToList();

            await _produktService.AddProdukt(nyProd, materialIds);

            MessageBox.Show("Sparad!", "Klar", MessageBoxButton.OK, MessageBoxImage.Information);

            // Nollställ formuläret
            NyttProduktNamn = string.Empty;
            NyttPris = 0;
            NyttLagerantal = 0;
            NyStorlek = string.Empty;
            NyTyp = string.Empty;
            NyModell = string.Empty;
            NyFärg = string.Empty;
            NyDecoration = string.Empty;
            ValdMaterial = null;
            NyMaterialLista.Clear();
        }
    }
}