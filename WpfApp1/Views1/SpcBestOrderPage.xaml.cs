using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WpfApp1.ViewModels;
using System.IO;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for SpcBestOrderPage.xaml
    /// </summary>
    public partial class SpcBestOrderPage : Page
    {
        public SpcBestOrderPage(SpcBestOrderPageVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void BtnLaddaUppBild_Click(object sender, RoutedEventArgs rev)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Tillåtna filnamn.Extensions (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
            fileDialog.InitialDirectory = System.IO.Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName, "DAL", "FörfråganBilder");

            if (fileDialog.ShowDialog() == true)
            {
                string filPath = fileDialog.FileName;

                BitmapImage bild = new BitmapImage();
                bild.BeginInit();
                bild.UriSource = new Uri(filPath);
                bild.CacheOption = BitmapCacheOption.OnLoad;
                bild.EndInit();

                //ImgElementIXaml.Source = bild;

                if (DataContext is SpcBestOrderPageVM vm)
                {
                    vm.BildUrl = filPath;
                    vm.ValdBild = bild;
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SparaSpecialbeställning_Click(object sender, RoutedEventArgs e)
        {            
        }

        private void ÖppnaFörfråganWindow_Clíck(object sender, RoutedEventArgs e)
        {
            FörfråganWindow fw = new FörfråganWindow();
            fw.Show();
        }
    }
}
