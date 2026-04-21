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
            MaterialLista = new ObservableCollection<Material>();
            _ = LaddaMaterial();
        }
        private async Task LaddaMaterial()
        {
            var material = await Task.Run(() => _materialRepo.GetAll());
            MaterialLista.Clear();
            foreach (var m in material)
            {
                MaterialLista.Add(m);
            }
        }
        // ===============================
        // 🔹 PROPERTIES (binding till UI)
        // ===============================

        [ObservableProperty]
        private ObservableCollection<Material> materialLista;

        [ObservableProperty]
        private Material selectedMaterial;

        [ObservableProperty]
        private ObservableCollection<BestallningsRad> bestallningsRader = new();

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
        private string statusMessage;

        [ObservableProperty]
        private bool isStatusVisible;

        [ObservableProperty]
        private bool isMaterialFormVisible = false;



        // ===============================
        // 🔹 COMMANDS
        // ===============================

        [RelayCommand]
        private async Task SkapaBestallning()
        {
            if (BestallningsRader.Count == 0)
                throw new Exception("Inga material i beställningen!");

            // 🔥 SÄTT FK HÄR
            foreach (var rad in BestallningsRader)
            {
                rad.MaterialId = rad.Material.MaterialID;
            }

            var bestallning = new MaterialBeställning
            {
                StartadAvID = Session.CurrentUser.AnvändarID,
                Rader = BestallningsRader.ToList(),
                TotalPris = BestallningsRader.Sum(r => r.RadPris)
            };

            senasteBestallning = bestallning;

            await _bestallningService.SkapaBestallning(bestallning);

            StatusMessage = "Beställning skapad!";
            IsStatusVisible = true;

            BestallningsRader.Clear();
        }

        [RelayCommand]
        private void AddToBestallning()
        {
            if (SelectedMaterial == null)
                throw new Exception("Välj material!");

            if (!int.TryParse(Antal, out int antal))
                throw new Exception("Fel antal!");

            BestallningsRader.Add(new BestallningsRad
            {
                Material = SelectedMaterial,
                Antal = antal
            });

            // rensa input
            Antal = "";
        }

        [RelayCommand]
        private void ExportPdf()
        {
            if (senasteBestallning == null)
                throw new Exception("Ingen beställning att exportera!");

            SparaTillPdf(senasteBestallning, Session.CurrentUser.Namn);
        }

        [RelayCommand]
        private void ToggleMaterialForm()
        {
            IsMaterialFormVisible = !IsMaterialFormVisible;
        }

        [RelayCommand]
        private async Task AddMaterial()
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

            await _materialRepo.Add(material);
            await _materialRepo.Save();

            // 🔄 Uppdatera listan direkt
            MaterialLista.Add(material);

            // 🔄 Rensa UI
            Namn = "";
            Pris = "";
            Typ = "";
        }
        private void SparaTillPdf(MaterialBeställning bestallning, string ansvarigNamn)
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
                    document.Add(new Paragraph($"Datum: {DateTime.Now}"));
                    document.Add(new Paragraph($"Ansvarig: {ansvarigNamn}"));
                    document.Add(new Paragraph(" "));

                    // 🔥 Loopa igenom ALLA rader
                    foreach (var rad in bestallning.Rader)
                    {
                        document.Add(new Paragraph($"Material: {rad.Material.Namn}"));
                        document.Add(new Paragraph($"Antal: {rad.Antal}"));
                        document.Add(new Paragraph($"Pris: {rad.RadPris}"));
                        document.Add(new Paragraph(" "));
                    }

                    // 🔥 TOTAL
                    document.Add(new Paragraph("----------------------"));
                    document.Add(new Paragraph($"TOTALPRIS: {bestallning.TotalPris}"));
                }
            }
        }
    }
}