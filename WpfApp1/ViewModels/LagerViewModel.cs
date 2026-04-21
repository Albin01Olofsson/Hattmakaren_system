using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL;
using DAL.Repositorys;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.ViewModels
{
    public partial class LagerViewModel : ObservableObject
    {
        private readonly MaterialRepo _materialRepo;
        private readonly ProduktRepo _produktRepo;

        [ObservableProperty]
        private ObservableCollection<Material> materialLista = new();

        [ObservableProperty]
        private ObservableCollection<Produkt> produktLista = new();

        [ObservableProperty]
        private ObservableCollection<Material> filtreradMaterialLista = new();

        [ObservableProperty]
        private Material selectedMaterial;

        [ObservableProperty]
        private Produkt selectedProdukt;

        [ObservableProperty]
        private bool isEditorVisible;

        [ObservableProperty]
        private bool isEditMode;

        [ObservableProperty]
        private bool isMaterialEditorVisible;

        [ObservableProperty]
        private bool isProduktEditorVisible;

        [ObservableProperty]
        private string editorTitel = "Nytt material";

        [ObservableProperty]
        private string materialNamn;

        [ObservableProperty]
        private string materialTyp;

        [ObservableProperty]
        private string materialPris;

        [ObservableProperty]
        private string materialSaldo;

        [ObservableProperty]
        private string produktNamn;

        [ObservableProperty]
        private string produktStorlek;

        [ObservableProperty]
        private string produktPris;

        [ObservableProperty]
        private string produktSaldo;

        public LagerViewModel()
        {
            var context = new DBcontext();

            _materialRepo = new MaterialRepo(context);
            _produktRepo = new ProduktRepo(context);

            _ = LoadDataSafe();
        }

        private async Task LoadDataSafe()
        {
            try
            {
                await LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Fel i LagerViewModel",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async Task LoadData()
        {
            var material = await _materialRepo.GetAll();
            MaterialLista = new ObservableCollection<Material>(material);
            FiltreradMaterialLista = new ObservableCollection<Material>(MaterialLista);

            var produkter = await _produktRepo.GetAll();
            ProduktLista = new ObservableCollection<Produkt>(produkter);
        }

        [RelayCommand]
        private void OppnaNyttMaterial()
        {
            IsEditMode = false;
            EditorTitel = "Nytt material";

            IsMaterialEditorVisible = true;
            IsProduktEditorVisible = false;

            MaterialNamn = "";
            MaterialTyp = "";
            MaterialPris = "";
            MaterialSaldo = "";

            IsEditorVisible = true;
        }

        [RelayCommand]
        private void OppnaNyProdukt()
        {
            IsEditMode = false;
            EditorTitel = "Ny hatt";

            IsMaterialEditorVisible = false;
            IsProduktEditorVisible = true;

            ProduktNamn = "";
            ProduktStorlek = "";
            ProduktPris = "";
            ProduktSaldo = "";

            IsEditorVisible = true;
        }

        [RelayCommand]
        private void OppnaRedigeraMaterial()
        {
            if (SelectedMaterial == null)
            {
                MessageBox.Show(
                    "Välj ett material i listan först.",
                    "Ingen rad markerad",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            IsEditMode = true;
            EditorTitel = "Redigera material";

            IsMaterialEditorVisible = true;
            IsProduktEditorVisible = false;

            MaterialNamn = SelectedMaterial.Namn;
            MaterialTyp = SelectedMaterial.Typ;
            MaterialPris = SelectedMaterial.Pris.ToString();
            MaterialSaldo = SelectedMaterial.Lagerantal.ToString();

            IsEditorVisible = true;
        }

        [RelayCommand]
        private void OppnaRedigeraProdukt()
        {
            if (SelectedProdukt == null)
            {
                MessageBox.Show(
                    "Välj en hatt i listan först.",
                    "Ingen rad markerad",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            IsEditMode = true;
            EditorTitel = "Redigera hatt";

            IsMaterialEditorVisible = false;
            IsProduktEditorVisible = true;

            ProduktNamn = SelectedProdukt.namn;
            ProduktStorlek = SelectedProdukt.Storlek;
            ProduktPris = SelectedProdukt.pris.ToString();
            ProduktSaldo = SelectedProdukt.Lagerantal.ToString();

            IsEditorVisible = true;
        }

        [RelayCommand]
        private void StangEditor()
        {
            IsEditorVisible = false;
            IsMaterialEditorVisible = false;
            IsProduktEditorVisible = false;
        }

        [RelayCommand]
        private async Task SparaMaterial()
        {
            if (string.IsNullOrWhiteSpace(MaterialNamn) ||
                string.IsNullOrWhiteSpace(MaterialTyp) ||
                string.IsNullOrWhiteSpace(MaterialPris) ||
                string.IsNullOrWhiteSpace(MaterialSaldo))
            {
                MessageBox.Show(
                    "Fyll i alla fält.",
                    "Ofullständig information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(MaterialPris, out decimal pris))
            {
                MessageBox.Show(
                    "Pris måste vara ett nummer.",
                    "Felaktigt pris",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(MaterialSaldo, out int saldo))
            {
                MessageBox.Show(
                    "Saldo måste vara ett heltal.",
                    "Felaktigt saldo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (IsEditMode)
            {
                if (SelectedMaterial == null)
                {
                    MessageBox.Show(
                        "Inget material valt.",
                        "Fel",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                var dbMaterial = await _materialRepo.GetById(SelectedMaterial.MaterialID);

                if (dbMaterial == null)
                {
                    MessageBox.Show(
                        "Materialet hittades inte.",
                        "Fel",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }



                dbMaterial.Namn = MaterialNamn;
                dbMaterial.Typ = MaterialTyp;
                dbMaterial.Pris = pris;
                dbMaterial.Lagerantal = saldo;

                await _materialRepo.Update(dbMaterial);
                await _materialRepo.Save();
            }
            else
            {
                var nyttMaterial = new Material
                {
                    Namn = MaterialNamn,
                    Typ = MaterialTyp,
                    Pris = pris,
                    Lagerantal = saldo,
                    Beskrivning = ""
                };

                await _materialRepo.Add(nyttMaterial);
                await _materialRepo.Save();
            }

            IsEditorVisible = false;
            IsMaterialEditorVisible = false;
            await LoadData();
        }
        [RelayCommand]
        private async Task SparaProdukt()
        {
            if (string.IsNullOrWhiteSpace(ProduktNamn) ||
                string.IsNullOrWhiteSpace(ProduktStorlek) ||
                string.IsNullOrWhiteSpace(ProduktPris) ||
                string.IsNullOrWhiteSpace(ProduktSaldo))
            {
                MessageBox.Show(
                    "Fyll i alla hattfält.",
                    "Ofullständig information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(ProduktPris, out decimal pris))
            {
                MessageBox.Show(
                    "Pris måste vara ett nummer.",
                    "Felaktigt pris",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(ProduktSaldo, out int saldo))
            {
                MessageBox.Show(
                    "Saldo måste vara ett heltal.",
                    "Felaktigt saldo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (IsEditMode)
            {
                if (SelectedProdukt == null)
                {
                    MessageBox.Show(
                        "Ingen hatt vald.",
                        "Fel",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                var dbProdukt = await _produktRepo.GetById(SelectedProdukt.ProduktID);

                if (dbProdukt == null)
                {
                    MessageBox.Show(
                        "Hatten hittades inte.",
                        "Fel",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                dbProdukt.namn = ProduktNamn;
                dbProdukt.Storlek = ProduktStorlek;
                dbProdukt.pris = pris;
                dbProdukt.Lagerantal = saldo;

                await _produktRepo.Update(dbProdukt);
                await _produktRepo.Save();
            }
            else
            {
                var nyProdukt = new Produkt
                {
                    namn = ProduktNamn,
                    Storlek = ProduktStorlek,
                    pris = pris,
                    Lagerantal = saldo,
                    Färdig = true,
                    HattTyp = "",
                    Modell = "",
                    Färg = "",
                    Decoration = "",
                    TillverkadAVID = Session.CurrentUser?.AnvändarID ?? 0
                };

                await _produktRepo.Add(nyProdukt);
                await _produktRepo.Save();
            }

            IsEditorVisible = false;
            IsProduktEditorVisible = false;

            ProduktNamn = "";
            ProduktStorlek = "";
            ProduktPris = "";
            ProduktSaldo = "";

            await LoadData();
        }
    }
}