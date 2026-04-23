using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ICollectionView MailsView { get; set; }
        public FörfrågningarPage(FörfrågningVM fVM)
        {
            InitializeComponent();

            MailsView = CollectionViewSource.GetDefaultView(fVM.Mails);
            DataContext = fVM;
        }

        private void SökResultat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FörfrågningarResultat.SelectedItem is Mail valdMail)
            {
                NavigationService.Navigate(new SeFörfrågningPage(valdMail));
            }
        }

        private void Sortering_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var valdSortering = (comboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            MailsView.SortDescriptions.Clear();

            if(valdSortering == "Nyast")
            {
                MailsView.SortDescriptions.Add(new SortDescription("Datum", ListSortDirection.Descending));
            }else if(valdSortering == "Äldst")
            {
                MailsView.SortDescriptions.Add(new SortDescription("Datum", ListSortDirection.Ascending));
            }
        }
    }
}
