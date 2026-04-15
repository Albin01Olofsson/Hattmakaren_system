using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage(OrderVM vm)
        {
            InitializeComponent();
            DataContext = vm;
        }


        private void BtnSök_Click(object sender, RoutedEventArgs ev)
        {
        }

        private void SökResultat_SelectionChanged(object sender, RoutedEventArgs ev)
        {
            if(SökResultat.SelectedItem is Order valdOrder)
            {
                NavigationService.Navigate(new OrderBeskrivningPage(valdOrder));
            }
        }

        private void BtnSkapaOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Vi hämtar vår "robot" (ServiceProvider) från App.xaml.cs
                var serviceProvider = ((App)Application.Current).ServiceProvider;

                // 2. Vi ber roboten bygga en helt färdig SkapaOrderViewModel
                // Den kommer automatiskt skicka in alla Services i konstruktorn!
                var viewModel = serviceProvider.GetRequiredService<SkapaOrderViewModel>();

                // 3. Vi skapar själva sidan och skickar med den färdiga ViewModeln
                var orderSida = new CreateOrderPage(viewModel);

                // 4. Vi utför själva navigeringen i fönstret
                this.NavigationService.Navigate(orderSida);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kunde inte öppna ordersidan: " + ex.Message);
            }
        }
    }
}
