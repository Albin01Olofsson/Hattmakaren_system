using BL.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL;
using DAL.Repositorys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.IO;

namespace WpfApp1.ViewModels
{
    public partial class BestallningarViewModel : ObservableObject
    {
        private readonly MaterialBeställningService _bestallningService;
        private readonly MaterialRepo _materialRepo;

        public BestallningarViewModel()
        {
            var context = new DBcontext();

            _bestallningService = new MaterialBeställningService(context); // ✅ RÄTT

            _materialRepo = new MaterialRepo(context);

            // 🔹 Ladda material till ComboBox
            MaterialLista = new ObservableCollection<Material>(_materialRepo.GetAll());
        }

        // ===============================
        // 🔹 PROPERTIES (binding till UI)
        // ===============================

        [ObservableProperty]
        private ObservableCollection<Material> materialLista;

        [ObservableProperty]
        private Material selectedMaterial;

        [ObservableProperty]
        private string antal;

        // 🔹 Nytt material
        [ObservableProperty]
        private string namn;

        [ObservableProperty]
        private string pris;

        [ObservableProperty]
        private string typ;

        [ObservableProperty]
        private bool isMaterialFormVisible;


        // ===============================
        // 🔹 COMMANDS
        // ===============================

        [RelayCommand]
        private void SkapaBestallning()
        {
            if (SelectedMaterial == null)
                throw new Exception("Välj material!");

            if (!int.TryParse(Antal, out int antal))
                throw new Exception("Mängd måste vara ett nummer!");

            if (Session.CurrentUser == null)
                throw new Exception("Ingen användare inloggad!");

            // 🔥 SKAPA OBJEKTET
            var bestallning = new MaterialBeställning
            {
                MaterialLista = new List<Material> { SelectedMaterial },
                TotalPris = SelectedMaterial.Pris * antal,
                StartadAvID = Session.CurrentUser.AnvändarID
            };

            // 🔹 Spara i databasen (din service)
            _bestallningService.SkapaBestallning(
                SelectedMaterial,
                antal,
                Session.CurrentUser.AnvändarID
            );

            // 🔹 Spara till TXT
            SparaTillTxt(bestallning, antal);

            // 🔄 Rensa UI
            Antal = "";
            SelectedMaterial = null;
        }

        [RelayCommand]
        private void ToggleMaterialForm()
        {
            IsMaterialFormVisible = !IsMaterialFormVisible;
        }

        [RelayCommand]
        private void AddMaterial()
        {
            if (string.IsNullOrWhiteSpace(Namn) || string.IsNullOrWhiteSpace(Typ))
                throw new Exception("Fyll i alla fält!");

            if (!decimal.TryParse(Pris, out decimal pris))
                throw new Exception("Pris måste vara ett nummer!");

            var material = new Material
            {
                Namn = Namn,
                Pris = pris,
                Typ = Typ,
                Beskrivning = ""
            };

            _materialRepo.Add(material);
            _materialRepo.Save();

            // 🔄 Uppdatera listan direkt
            MaterialLista.Add(material);

            // 🔄 Rensa UI
            Namn = "";
            Pris = "";
            Typ = "";
        }
        private void SparaTillTxt(MaterialBeställning bestallning, int antal)
        {
            string path = "bestallningar.txt";

            string text = $"Material: {bestallning.MaterialLista[0].Namn}, " +
                          $"Antal: {antal}, " +
                          $"Pris: {bestallning.TotalPris}, " +
                          $"Datum: {DateTime.Now}";

            File.AppendAllText(path, text + Environment.NewLine);
        }
    }
}