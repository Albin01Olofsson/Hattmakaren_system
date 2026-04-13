using System.Windows.Controls;
using WpfApp1.ViewModels; // Se till att detta matchar mappen där din ViewModel ligger

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for CreateOrderPage.xaml
    /// </summary>
    public partial class CreateOrderPage : Page
    {

        public CreateOrderPage(SkapaOrderViewModel viewModel)
        {
            InitializeComponent();


            this.DataContext = viewModel;
        }
    }
}