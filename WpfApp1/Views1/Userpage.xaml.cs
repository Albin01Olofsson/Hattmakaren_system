using BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    public partial class AnvandarePage : Page
    {
       private readonly IAnvändarService _användarService;
        public AnvandarePage()
        {
            InitializeComponent();
            var serviceProvider = ((App)Application.Current).ServiceProvider;
            _användarService = serviceProvider.GetRequiredService<IAnvändarService>();
            var vm = new AddAnvändareViewModel();
            vm.AnvändareAdded += (användare) =>
            {
                _användarService.LäggTillAnvändare(användare);
                MessageBox.Show("Användare sparad!");
            };
            DataContext = vm;
            SetupView();
        }

        private void SetupView()
        {
            var användare = Session.CurrentUser;

            if(användare == null)
            {
                AdminPanel.Visibility = Visibility.Collapsed;
                BtnDeleteStaff.Visibility = Visibility.Collapsed;
                BtnDeleteCustomer.Visibility = Visibility.Collapsed;

                return;
            }

            // Kontrollera att namnen (AdminPanel, BtnDeleteStaff osv) matchar x:Name i din XAML
            if (användare.IsAdmin)
            {
                AdminPanel.IsEnabled = true;
                BtnDeleteStaff.IsEnabled = true;
                BtnDeleteCustomer.IsEnabled = true;
                TxtHeaderTitle.Text = "SYSTEMADMINISTRATION";
                TxtFormTitle.Text = "REGISTRERA KONTO";
            }
            else
            {
                AdminPanel.IsEnabled = false;
                BtnDeleteStaff.IsEnabled = false;
                BtnDeleteCustomer.IsEnabled = false;

                TxtHeaderTitle.Text = "MINA INSTÄLLNINGAR";
                TxtHeaderSub.Text = "Här kan du hantera din profil och se kundregistret";
                TxtFormTitle.Text = "DIN PROFIL";
            }
        }
    }
}