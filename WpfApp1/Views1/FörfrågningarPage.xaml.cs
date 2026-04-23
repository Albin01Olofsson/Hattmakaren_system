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

        public List<Mail> AllaMail;
        public FörfrågningarPage(FörfrågningVM fVM)
        {
            InitializeComponent();

            MailsView = CollectionViewSource.GetDefaultView(fVM.Mails);
            DataContext = fVM;

            AllaMail = fVM.Mails.ToList();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext is FörfrågningVM vm)
            {
                await vm.LoadMails();
                AllaMail = vm.Mails.ToList();
            }
        }

        private void BtnSök_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is not FörfrågningVM vm)
                return;

            string sökString = SökFält.Text.ToLower();

            var filtreradLista = AllaMail.Where(m => string.IsNullOrWhiteSpace(sökString) ||
            (m.Ämne?.ToLower().Contains(sökString) ?? false) ||
            (m.Avsändare?.ToLower().StartsWith(sökString) ?? false)).ToList();

            vm.Mails.Clear();

            foreach (var mail in filtreradLista)
                vm.Mails.Add(mail);
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
