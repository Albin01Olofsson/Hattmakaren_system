using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    public partial class Mainpage : Page
    {
        public Mainpage()
        {
            InitializeComponent();
        }

        // Här är koden som körs när du klickar på "Användare"
        private void BtnAnvandare_Click(object sender, RoutedEventArgs e)
        {
            // 1. Dölj startvyn (Dashboarden)
            DashboardStartView.Visibility = Visibility.Collapsed;

            // 2. Öppna användarsidan. 
            // Ändra "Admin" till "Personal" här om du vill se hur det ser ut för anställda
            MainFrame.Navigate(new AnvandarePage());
        }

        private void BtnKunder_Click(object sender, RoutedEventArgs e)
        {
            // Här kan du lägga till navigering för Kunder senare
            MainFrame.Navigate(new KunderPage());
        }

        private void BtnLager_Click(object sender, RoutedEventArgs e)
        {
            // Här kan du lägga till navigering för Lager senare
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            // Här kan du lägga till navigering för Order senare
            MainFrame.Navigate(new OrderPage());
        }

        private void BtnBestallningar_Click(object sender, RoutedEventArgs e)
        {
            DashboardStartView.Visibility = Visibility.Collapsed;
            MainFrame.Navigate(new BestallningarPage());
        }

        private void BtnLoggaUt_Click(object sender, RoutedEventArgs e)
        {
            
            Session.CurrentUser = null;// Rensa sessionen
            // Navigera till inloggningssidan
            var app = (App)Application.Current;// Hämta applikationsinstansen för att komma åt DI-container
            var vm = (LoginViewModel)app.ServiceProvider.GetService(typeof(LoginViewModel));// Hämta LoginViewModel från DI-container
            var loginPage = new LoginPage
            {
                DataContext = vm// Sätt DataContext för inloggningssidan
            };

            vm.LoginSucceeded += () =>// Prenumerera på inloggningshändelsen
            {
                ((MainWindow)Application.Current.MainWindow)
                .MainFrame.Navigate(new Mainpage());// Navigera tillbaka till dashboarden efter inloggning
            };
            this.NavigationService.Navigate(loginPage);// Navigera till inloggningssidan
        }
    }
}