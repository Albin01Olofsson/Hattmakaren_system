using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL;
using DAL.Repositorys;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public partial class LagerViewModel : ObservableObject
    {
        private readonly MaterialRepo _materialRepo;

        [ObservableProperty]
        private ObservableCollection<Material> materialLista = new();

        [ObservableProperty]
        private string namn;

        [ObservableProperty]
        private string pris;

        [ObservableProperty]
        private string typ;

        [ObservableProperty]
        private string lagerantal;

        public LagerViewModel()
        {
            var context = new DBcontext();
            _materialRepo = new MaterialRepo(context);

            _ = LoadMaterial();
        }

        private async Task LoadMaterial()
        {
            var material = await _materialRepo.GetAll();
            MaterialLista = new ObservableCollection<Material>(material);
        }

        [RelayCommand]
        private async Task AddMaterial()
        {
            if (string.IsNullOrWhiteSpace(Namn) ||
                string.IsNullOrWhiteSpace(Pris) ||
                string.IsNullOrWhiteSpace(Typ) ||
                string.IsNullOrWhiteSpace(Lagerantal))
            {
                throw new Exception("Fyll i alla fält!");
            }

            if (!decimal.TryParse(Pris, out decimal pris))
                throw new Exception("Pris måste vara ett nummer!");

            if (!int.TryParse(Lagerantal, out int lagerantal))
                throw new Exception("Lagerantal måste vara ett heltal!");

            var material = new Material
            {
                Namn = Namn,
                Pris = pris,
                Typ = Typ,
                Beskrivning = "",
                Lagerantal = lagerantal
            };

            _materialRepo.Add(material);
            await _materialRepo.Save();

            await LoadMaterial();

            Namn = "";
            Pris = "";
            Typ = "";
            Lagerantal = "";
        }
    }
}