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

        private void KunderUpdate(object s, EventArgs e)
        {
            if(this.DataContext is SkapaOrderViewModel viewModel)
            {
                    _ = viewModel.LaddaDataCommand.ExecuteAsync(null);
            }
        }

        private void BtnSkapaLagerfördProdukt_Click(object sender, RoutedEventArgs rea)
        {
            var orderContext = new DBcontext();

            NavigationService.Navigate(new SkapaLagerfördProdukt(new SkapaLagerfördProduktVM(
                    new OrderService(new OrderRepo(orderContext), orderContext),
                    new ProduktService(new ProduktRepo(orderContext), orderContext),
                    new MaterialService(new MaterialRepo(orderContext)),
                    new KundService(new KundRepo(orderContext)),
                    new AnvändarService(new AnvändarRepo(orderContext))
                )));
        }

        private void BtnSkapaSpecialbeställning_Click(object sender, RoutedEventArgs rea)
        {
            var orderContext = new DBcontext();

            NavigationService.Navigate(new SpcBestOrderPage(new SpcBestOrderPageVM(
                    new OrderService(new OrderRepo(orderContext), orderContext),
                    new ProduktService(new ProduktRepo(orderContext), orderContext),
                    new MaterialService(new MaterialRepo(orderContext)),
                    new KundService(new KundRepo(orderContext)),
                    new AnvändarService(new AnvändarRepo(orderContext))
                )));
        }

    }
}