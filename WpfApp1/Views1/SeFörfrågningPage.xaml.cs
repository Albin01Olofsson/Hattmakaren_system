using System;
using System.Collections.Generic;
using System.IO;
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
using Models;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for SeFörfrågningPage.xaml
    /// </summary>
    public partial class SeFörfrågningPage : Page
    {
        public SeFörfrågningPage(Mail valdMail)
        {
            InitializeComponent();
            DataContext = valdMail;

            if(!string.IsNullOrWhiteSpace(valdMail.BildSökVäg) && File.Exists(valdMail.BildSökVäg))
            {
                var bitMap = new BitmapImage();

                using (var stream = new FileStream(valdMail.BildSökVäg, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    bitMap.BeginInit();
                    bitMap.CacheOption = BitmapCacheOption.OnLoad;
                    bitMap.StreamSource = stream;
                    bitMap.EndInit();
                    bitMap.Freeze();
                }
                MailBild.Source = bitMap;
            }
        }
    }
}
