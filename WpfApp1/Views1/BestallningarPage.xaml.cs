using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Intefaces;
using DAL.Repositorys;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


    namespace WpfApp1.Views1
    {
    public partial class BestallningarPage : Page
    {
        private readonly DBcontext _context;
        private readonly MaterialBeställningRepo _bestallningRepo;
        private readonly MaterialRepo _materialRepo;
        private readonly MaterialBeställningService _bestallningService;

        public BestallningarPage()
        {
            InitializeComponent();

            _context = new DBcontext();

            _bestallningRepo = new MaterialBeställningRepo(_context);
            _materialRepo = new MaterialRepo(_context);

            _bestallningService = new MaterialBeställningService(_bestallningRepo);

            LoadMaterial();
        }




        private void BtnSkapa_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialComboBox.SelectedItem == null)
            {
                MessageBox.Show("Välj material!");
                return;
            }

            if (!int.TryParse(TxtAntal.Text, out int antal))
            {
                MessageBox.Show("Mängd måste vara ett nummer!");
                return;
            }

            var valtMaterial = (Material)MaterialComboBox.SelectedItem;

            try
            {
                _bestallningService.SkapaBestallning(valtMaterial, antal);
                MessageBox.Show("Beställning sparad!");

                MaterialComboBox.SelectedItem = null;
                TxtAntal.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ToggleMaterialForm_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialForm.Visibility == Visibility.Collapsed)
                MaterialForm.Visibility = Visibility.Visible;
            else
                MaterialForm.Visibility = Visibility.Collapsed;
        }
        private void BtnAddMaterial_Click(object sender, RoutedEventArgs e)
        {
            string namn = TxtNamn.Text;
            string typ = TxtTyp.Text;

            if (!decimal.TryParse(TxtPris.Text, out decimal pris))
            {
                MessageBox.Show("Pris måste vara ett nummer!");
                return;
            }

            if (string.IsNullOrWhiteSpace(namn) || string.IsNullOrWhiteSpace(typ))
            {
                MessageBox.Show("Fyll i alla fält!");
                return;
            }

            var material = new Material
            {
                Namn = namn,
                Pris = pris,
                Typ = typ,
                Beskrivning = ""
            };

            _materialRepo.Add(material);
            _materialRepo.Save(); // 🔥 rätt repo

            MessageBox.Show("Material tillagt!");

            LoadMaterial();

            TxtNamn.Clear();
            TxtPris.Clear();
            TxtTyp.Clear();

            MaterialForm.Visibility = Visibility.Collapsed;
        }

        private void LoadMaterial()
        {
            try
            {
                MaterialComboBox.ItemsSource = _materialRepo.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}

