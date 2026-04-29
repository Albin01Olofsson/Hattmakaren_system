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
        private string materialPris;

        [ObservableProperty]
        private string materialSaldo;

        public Array MåttTyper => Enum.GetValues(typeof(MåttTyp));

        [ObservableProperty]
        private MåttTyp selectedMåttTyp;

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
                MessageBox.Show(ex.ToString(), "Fel i LagerViewModel");
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

        // =========================
        // 🔹 MATERIAL
        // =========================

        [RelayCommand]
        private void OppnaNyttMaterial()
        {
            IsEditMode = false;
            EditorTitel = "Nytt material";

            IsMaterialEditorVisible = true;
            IsProduktEditorVisible = false;

            MaterialNamn = "";
            MaterialPris = "";
            MaterialSaldo = "";
            SelectedMåttTyp = default;

            IsEditorVisible = true;
        }

        [RelayCommand]
        private async Task OppnaRedigeraMaterial()
        {
            if (SelectedMaterial == null)
            {
                MessageBox.Show("Välj ett material först.");
                return;
            }

            IsEditMode = true;
            EditorTitel = "Redigera material";

            IsMaterialEditorVisible = true;
            IsProduktEditorVisible = false;

            MaterialNamn = SelectedMaterial.Namn;
            MaterialPris = SelectedMaterial.Pris.ToString();
            MaterialSaldo = SelectedMaterial.Lagerantal.ToString();
            SelectedMåttTyp = SelectedMaterial.MåttTyp;

            IsEditorVisible = true;

            await _materialRepo.Update(SelectedMaterial);
            await _materialRepo.Save();

        }

        [RelayCommand]
        private async Task SparaMaterial()
        {
            if (string.IsNullOrWhiteSpace(MaterialNamn) ||
                string.IsNullOrWhiteSpace(MaterialPris) ||
                string.IsNullOrWhiteSpace(MaterialSaldo))
            {
                MessageBox.Show("Fyll i alla fält.");
                return;
            }

            if (!decimal.TryParse(MaterialPris, out decimal pris))
            {
                MessageBox.Show("Pris måste vara ett nummer.");
                return;
            }

            if (!int.TryParse(MaterialSaldo, out int saldo))
            {
                MessageBox.Show("Saldo måste vara ett heltal.");
                return;
            }

            if (IsEditMode)
            {
                var dbMaterial = await _materialRepo.GetById(SelectedMaterial.MaterialID);

                dbMaterial.Namn = MaterialNamn;
                dbMaterial.MåttTyp = SelectedMåttTyp;
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
                    MåttTyp = SelectedMåttTyp,
                    Pris = pris,
                    Lagerantal = saldo,
                    Beskrivning = ""
                };

                await _materialRepo.Add(nyttMaterial);
                await _materialRepo.Save();
            }

            IsEditorVisible = false;
            await LoadData();
        }

        // =========================
        // 🔹 PRODUKT
        // =========================

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
        private void OppnaRedigeraProdukt()
        {
            if (SelectedProdukt == null)
            {
                MessageBox.Show("Välj en produkt först.");
                return;
            }

            IsEditMode = true;
            EditorTitel = "Redigera hatt";

            IsMaterialEditorVisible = false;
            IsProduktEditorVisible = true;

            ProduktNamn = SelectedProdukt.Namn;
            ProduktStorlek = SelectedProdukt.Storlek;
            ProduktPris = SelectedProdukt.Pris.ToString();
            ProduktSaldo = SelectedProdukt.Lagerantal.ToString();

            IsEditorVisible = true;
        }

        [RelayCommand]
        private async Task SparaProdukt()
        {
            if (string.IsNullOrWhiteSpace(ProduktNamn) ||
                string.IsNullOrWhiteSpace(ProduktStorlek) ||
                string.IsNullOrWhiteSpace(ProduktPris) ||
                string.IsNullOrWhiteSpace(ProduktSaldo))
            {
                MessageBox.Show("Fyll i alla fält.");
                return;
            }

            if (!decimal.TryParse(ProduktPris, out decimal pris))
            {
                MessageBox.Show("Pris måste vara ett nummer.");
                return;
            }

            if (!int.TryParse(ProduktSaldo, out int saldo))
            {
                MessageBox.Show("Saldo måste vara ett heltal.");
                return;
            }

            if (IsEditMode)
            {
                var dbProdukt = await _produktRepo.GetById(SelectedProdukt.ProduktID);

                dbProdukt.Namn = ProduktNamn;
                dbProdukt.Storlek = ProduktStorlek;
                dbProdukt.Pris = pris;
                dbProdukt.Lagerantal = saldo;

                await _produktRepo.Update(dbProdukt);
                await _produktRepo.Save();
            }
            else
            {
                var nyProdukt = new Produkt
                {
                    Namn = ProduktNamn,
                    Storlek = ProduktStorlek,
                    Pris = pris,
                    Lagerantal = saldo,
                    Färdig = true,
                    TillverkadAVID = Session.CurrentUser?.AnvändarID ?? 0
                };

                await _produktRepo.Add(nyProdukt);
                await _produktRepo.Save();
            }

            IsEditorVisible = false;
            await LoadData();
        }

        [RelayCommand]
        private void StangEditor()
        {
            IsEditorVisible = false;
        }
    }
}