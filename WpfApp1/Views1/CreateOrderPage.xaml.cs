using BL.Interfaces;
using BL.Services;
using DAL.Repositorys;
using DAL;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels; // Se till att detta matchar mappen där din ViewModel ligger

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for CreateOrderPage.xaml
    /// </summary>
    public partial class CreateOrderPage : Page
    {
        private readonly IKundService _kundService;

        public CreateOrderPage(SkapaOrderViewModel viewModel, IKundService kundService)
        {
            InitializeComponent();

            _kundService = kundService;

            this.DataContext = viewModel;
        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var window = new AddKundWindow();
            if (window.ShowDialog() == true)
            {
                _kundService.AddKund(window.CreatedKund);
                var vm = (SkapaOrderViewModel)this.DataContext;
                vm.AllaKunder.Add(window.CreatedKund);
                MessageBox.Show("Kund tillagd!");//hej
            }
        }



        private void BtnSkapaLagerfördProdukt_Click(object sender, RoutedEventArgs rea)
        {
            NavigationService.Navigate(new SkapaLagerfördProdukt());
        }

        private void BtnSkapaSpecialbeställning_Click(object sender, RoutedEventArgs rea)
        {
            NavigationService.Navigate(new SpcBestOrderPage(new SpcBestOrderPageVM(
                    new OrderService(new OrderRepo(new DBcontext())),
                    new ProduktService(new ProduktRepo(new DBcontext())),
                    new MaterialService(new MaterialRepo(new DBcontext())),
                    new KundService(new KundRepo(new DBcontext())),
                    new AnvändarService(new AnvändarRepo(new DBcontext()))
                )));
        }

    }
}