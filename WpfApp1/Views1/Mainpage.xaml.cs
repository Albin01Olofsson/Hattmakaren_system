using System;
using System.Windows;
using System.Windows.Controls;

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
            MainFrame.Navigate(new AnvandarePage("Admin"));
        }

        private void BtnKunder_Click(object sender, RoutedEventArgs e)
        {
            // Här kan du lägga till navigering för Kunder senare
            // MainFrame.Navigate(new KunderPage());
        }

        private void BtnLager_Click(object sender, RoutedEventArgs e)
        {
            // Här kan du lägga till navigering för Lager senare
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            // Här kan du lägga till navigering för Order senare
        }

        private void BtnBestallningar_Click(object sender, RoutedEventArgs e)
        {
            DashboardStartView.Visibility = Visibility.Collapsed;
            MainFrame.Navigate(new BestallningarPage());
        }

        private void BtnLoggaUt_Click(object sender, RoutedEventArgs e)
        {
            // Går tillbaka till inloggningssidan
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new Uri("Views1/LoginPage.xaml", UriKind.Relative));
            }
        }
    }
}