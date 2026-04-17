using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    /// <summary>
    /// Interaction logic for SpcBestOrderPage.xaml
    /// </summary>
    public partial class SpcBestOrderPage : Page
    {
        public SpcBestOrderPage()
        {
            InitializeComponent();
        }

        private void BtnLaddaUppBild_Click(object sender, RoutedEventArgs rev)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Tillåtna filnamn.Extensions (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            if (fileDialog.ShowDialog() == true)
            {
                string filPath = fileDialog.FileName;

                BitmapImage bild = new BitmapImage();
                bild.BeginInit();
                bild.UriSource = new Uri(filPath);
                bild.CacheOption = BitmapCacheOption.OnLoad;
                bild.EndInit();

                ImgElementIXaml.Source = bild;
            }
        }

    }
}
