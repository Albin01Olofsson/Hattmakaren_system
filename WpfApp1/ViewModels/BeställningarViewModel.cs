using BL.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL;
using DAL.Repositorys;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Win32;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Views1;

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
        private Brush statusColor = Brushes.White;

        [ObservableProperty]
        private ObservableCollection<BestallningsRad> bestallningsRader = new();

        [ObservableProperty]
        private string antal;

        [ObservableProperty]
        private string leverantörNamn;

        // 🔹 Nytt material
        [ObservableProperty]
        private string namn;

        [ObservableProperty]
        private string pris;

        public Array MåttTyper => Enum.GetValues(typeof(MåttTyp));

        [ObservableProperty]
        private MåttTyp selectedMåttTyp;

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
            {
                StatusMessage = "❌ Lägg till minst ett material innan du skapar beställning!";
                StatusColor = Brushes.Red;
                IsStatusVisible = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(LeverantörNamn))
            {
                StatusMessage = "Ange leverantör!";
                return;
            }

            foreach (var rad in BestallningsRader)
            {
                rad.MaterialId = rad.Material.MaterialID;
            }

            var bestallning = new MaterialBeställning
            {
                Datum = DateTime.Now,
                StartadAvID = Session.CurrentUser.AnvändarID,
                Rader = BestallningsRader.ToList(),
                Leverantör = LeverantörNamn,
                TotalPris = BestallningsRader.Sum(r => r.RadPris)
                
            };

            // 🔥 Sätt innan await
            senasteBestallning = bestallning;

            await _bestallningService.SkapaBestallning(bestallning);

            StatusMessage = "✅ Beställning skapad!";
            StatusColor = Brushes.LightGreen;
            IsStatusVisible = true;

            BestallningsRader.Clear();
        }

        [RelayCommand]
        private void NavigateToBestallningarLista()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            var mainPage = window.MainFrame.Content as Mainpage;

            mainPage?.GetFrame().Navigate(new BestallningarListaPage());
        }

        [RelayCommand]
        private void AddToBestallning()
        {
            if (SelectedMaterial == null)
            {
                StatusMessage = "❌ Välj ett material!";
                StatusColor = Brushes.Red;
                IsStatusVisible = true;
                return;
            }

            if (!int.TryParse(Antal, out int antal))
            {
                StatusMessage = "❌ Ange ett giltigt antal!";
                StatusColor = Brushes.Red;
                IsStatusVisible = true;
                return;
            }

            BestallningsRader.Add(new BestallningsRad
            {
                Material = SelectedMaterial,
                Antal = antal
            });

            Antal = "";
        }

        [RelayCommand]
        private void ExportPdf()
        {
            if (senasteBestallning == null)
            {
                StatusMessage = "❌ Skapa en beställning först!";
                StatusColor = Brushes.Red;
                IsStatusVisible = true;
                return;
            }

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
            if (string.IsNullOrWhiteSpace(Namn))
                throw new Exception("Fyll i namn!");

            if (!decimal.TryParse(Pris, out decimal pris))
                throw new Exception("Pris måste vara ett nummer!");

            var material = new Material
            {
                Namn = Namn,
                Pris = pris,
                MåttTyp = SelectedMåttTyp,
                Beskrivning = ""
            };

            await _materialRepo.Add(material);
            await _materialRepo.Save();

            MaterialLista.Add(material);

            // 🔄 Rensa UI
            Namn = "";
            Pris = "";
            SelectedMåttTyp = default;
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
                    document.Add(new Paragraph($"Leverantör: {bestallning.Leverantör}"));
                    document.Add(new Paragraph(" "));

                    foreach (var rad in bestallning.Rader)
                    {
                        document.Add(new Paragraph($"Material: {rad.Material.Namn}"));
                        document.Add(new Paragraph($"Antal: {rad.Antal} {rad.Material.MåttText}"));
                        document.Add(new Paragraph($"Pris: {rad.RadPris}"));
                        document.Add(new Paragraph(" "));
                    }

                    document.Add(new Paragraph("----------------------"));
                    document.Add(new Paragraph($"TOTALPRIS: {bestallning.TotalPris}"));
                }
            }
        }
    }
}