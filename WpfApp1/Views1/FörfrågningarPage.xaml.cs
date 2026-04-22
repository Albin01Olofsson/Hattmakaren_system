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
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for FörfrågningarPage.xaml
    /// </summary>
    public partial class FörfrågningarPage : Page
    {
        public FörfrågningarPage(FörfrågningVM fVM)
        {
            InitializeComponent();
            DataContext = fVM;
        }

        private void SökResultat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
