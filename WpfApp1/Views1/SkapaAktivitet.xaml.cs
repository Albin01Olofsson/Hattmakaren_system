using Microsoft.Extensions.DependencyInjection;
using Models;
using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for SkapaAktivitet.xaml
    /// </summary>
    public partial class SkapaAktivitet : Window
    {
        public Planering CreatedPlanering { get; private set; }
        public SkapaAktivitet()
        {
            InitializeComponent();

            // Hämta hela ViewModellen från din ServiceProvider istället för att skriva 'new'
            var serviceProvider = ((App)Application.Current).ServiceProvider;
            var vm = serviceProvider.GetRequiredService<PlaneringViewModel>();

            vm.User = Session.CurrentUser;

            DataContext = vm;
            vm.PlaneringAdded += (planering) =>
            {
                CreatedPlanering = planering;
                this.DialogResult = true; // Stänger fönstret och indikerar att en planering skapades
                Close();
            };
        }
    }
}
