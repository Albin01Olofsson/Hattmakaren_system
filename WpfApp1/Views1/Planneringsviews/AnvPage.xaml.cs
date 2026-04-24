using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1.Planneringsviews
{
    /// <summary>
    /// Interaction logic for AnvPage.xaml
    /// </summary>
    public partial class AnvPage : Page
    {
        public AnvPage()
        {
            InitializeComponent();
            //this.DataContext = new WpfApp1.ViewModels.AnvPlanViewModel();
            var serviceProvider = ((App)Application.Current).ServiceProvider;
            this.DataContext = serviceProvider.GetRequiredService<AnvPlanViewModel>();

        }
        private void Ordrar_Click(object sender, RoutedEventArgs e)
        {
            // Vi skapar en ny instans av fönstret
            var skapaFönster = new SkapaAktivitet();

            if(skapaFönster.ShowDialog() == true)
            {
                var vm = (AnvPlanViewModel)DataContext;
                vm.LaddaSchema();
            }
            // ShowDialog() gör att man måste stänga fönstret innan man kan trycka på schemat igen
            // Show() låter båda fönstren vara öppna samtidigt
            //skapaFönster.ShowDialog();
        }

        
    }
}
