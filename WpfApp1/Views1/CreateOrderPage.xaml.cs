using BL.Interfaces;
using BL.Services;
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
                MessageBox.Show("Kund tillagd!");//hej
            }
        }

    }
}