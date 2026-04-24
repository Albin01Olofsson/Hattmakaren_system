using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Repositorys;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
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

            var orderService = new OrderService(
        new OrderRepo(orderContext),
        orderContext,
        new ProduktRepo(orderContext)
    );

            NavigationService.Navigate(
                new SkapaLagerfördProdukt(
                    new SkapaLagerfördProduktVM(
                        orderService,
                        new ProduktService(new ProduktRepo(orderContext)),
                        new MaterialService(new MaterialRepo(orderContext)),
                        new KundService(new KundRepo(orderContext)),
                        new AnvändarService(new AnvändarRepo(orderContext))
                    )
                )
            );
            //NavigationService.Navigate(new SkapaLagerfördProdukt(new SkapaLagerfördProduktVM(
            //        new OrderService(new OrderRepo(orderContext), orderContext),
            //        new ProduktService(new ProduktRepo(new DBcontext())),
            //        new MaterialService(new MaterialRepo(new DBcontext())),
            //        new KundService(new KundRepo(new DBcontext())),
            //        new AnvändarService(new AnvändarRepo(new DBcontext()))
            //    )));
        }

        private void BtnSkapaSpecialbeställning_Click(object sender, RoutedEventArgs rea)
        {
            var orderContext = new DBcontext();
            var orderService = new OrderService(
        new OrderRepo(orderContext),
        orderContext,
        new ProduktRepo(orderContext)
    );

            var produktService = new ProduktService(
                new ProduktRepo(orderContext)
            );

            var materialService = new MaterialService(
                new MaterialRepo(orderContext)
            );

            var kundService = new KundService(
                new KundRepo(orderContext)
            );

            var användarService = new AnvändarService(
                new AnvändarRepo(orderContext)
            );

            var vm = new SpcBestOrderPageVM(
                orderService,
                produktService,
                materialService,
                kundService,
                användarService
            );

            NavigationService.Navigate(new SpcBestOrderPage(vm));
            //NavigationService.Navigate(new SpcBestOrderPage(new SpcBestOrderPageVM(
            //        new OrderService(new OrderRepo(orderContext), orderContext),
            //        new ProduktService(new ProduktRepo(new DBcontext())),
            //        new MaterialService(new MaterialRepo(new DBcontext())),
            //        new KundService(new KundRepo(new DBcontext())),
            //        new AnvändarService(new AnvändarRepo(new DBcontext()))
            //    )));
        }

        private void BtnSkapaArtikel_Click(object sender, RoutedEventArgs e)
        {
            var sp = ((App)Application.Current).ServiceProvider;
            var vm = sp.GetRequiredService<SkapaArtikelVM>();

            NavigationService.Navigate(new SkapaArtikel(vm));
        }
    }
}