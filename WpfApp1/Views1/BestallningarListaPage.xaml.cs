using Models;
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
    public partial class BestallningarListaPage : Page
    {
        public BestallningarListaPage()
        {
            InitializeComponent();
            DataContext = new BestallningarListaViewModel();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox combo && combo.DataContext is MaterialBeställning bestallning)
            {
                if (DataContext is BestallningarListaViewModel vm)
                {
                    vm.UpdateLevereradCommand.Execute(bestallning);
                }
            }
        }
    }
}