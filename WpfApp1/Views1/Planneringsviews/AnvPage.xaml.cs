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
        private async void Ordrar_Click(object sender, RoutedEventArgs e)
        {
            var skapaFönster = new SkapaAktivitet();

            if (skapaFönster.ShowDialog() == true)
            {
                var vm = (AnvPlanViewModel)DataContext;


                await vm.LaddaSchema();
            }
        }

        private async void Kalender_AppointmentDropping(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentDroppingEventArgs e)
        {
            // 1. Dra ut det specifika SchemaBlock som användaren precis flyttade
            if (e.Appointment.Data is WpfApp1.ViewModels.SchemaBlock flyttatBlock)
            {
                // 2. Räkna ut hur lång aktiviteten är (så att sluttiden flyttas med korrekt)
                TimeSpan längd = flyttatBlock.SlutTid - flyttatBlock.StartTid;

                DateTime nyStart = e.DropTime;
                DateTime nySlut = nyStart + längd;

                // 3. Få tag i din ViewModel
                var vm = (WpfApp1.ViewModels.AnvPlanViewModel)this.DataContext;

                // 4. Spara till databasen
                await vm.UppdateraTid(flyttatBlock.Id, flyttatBlock.Typ, nyStart, nySlut);
            }
        }


    }
}
