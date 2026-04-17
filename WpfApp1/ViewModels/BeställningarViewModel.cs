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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Win32;

namespace WpfApp1.ViewModels
{
    public partial class BestallningarViewModel : ObservableObject
    {
        private readonly MaterialBeställningService _bestallningService;
        private readonly MaterialRepo _materialRepo;
        private MaterialBeställning senasteBestallning;

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
                StartadAvID = Session.CurrentUser.AnvändarID,
                Antal = antal
            };

            senasteBestallning = bestallning;

            // 🔹 Spara i databasen (din service)
            _bestallningService.SkapaBestallning(
                SelectedMaterial,
                antal,
                Session.CurrentUser.AnvändarID
            );
        }

        [RelayCommand]
        private void ExportPdf()
        {
            if (senasteBestallning == null)
                throw new Exception("Ingen beställning att exportera!");

            if (!int.TryParse(Antal, out int antal))
                throw new Exception("Fel antal!");

            SparaTillPdf(senasteBestallning, antal);
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
        private void SparaTillPdf(MaterialBeställning bestallning, int antal)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "bestallning.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                using (var writer = new PdfWriter(dialog.FileName))
                using (var pdf = new PdfDocument(writer))
                using (var document = new Document(pdf))
                {
                    document.Add(new Paragraph("Materialbeställning"));
                    document.Add(new Paragraph("----------------------"));

                    document.Add(new Paragraph($"Material: {bestallning.MaterialLista[0].Namn}"));
                    document.Add(new Paragraph($"Antal: {antal}"));
                    document.Add(new Paragraph($"Pris: {bestallning.TotalPris}"));
                    document.Add(new Paragraph($"Datum: {DateTime.Now}"));
                }
            }
        }
    }
}