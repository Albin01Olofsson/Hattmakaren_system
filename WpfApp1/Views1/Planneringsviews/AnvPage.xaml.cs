using BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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
using WpfApp1.ViewModels;

namespace WpfApp1.Views1.Planneringsviews
{
    /// <summary>
    /// Interaction logic for AnvPage.xaml
    /// </summary>
    public partial class AnvPage : Page
    {
        private IOrderService service;
        public AnvPage()
        {
            InitializeComponent();
            var serviceProvider = ((App)Application.Current).ServiceProvider;
            service = serviceProvider.GetRequiredService<IOrderService>();
            this.DataContext = new WpfApp1.ViewModels.AnvPlanViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new SkapaAktivitet();
            if (window.ShowDialog() == true)
            {
                service.PlaneraArbete(window.CreatedPlanering);
                var vm = (AnvPlanViewModel)this.DataContext;
                //vm.Bokningar.Add(window.CreatedPlanering);
                MessageBox.Show("Aktivitet tillagd!");//hej
            }
        }
    }
}
