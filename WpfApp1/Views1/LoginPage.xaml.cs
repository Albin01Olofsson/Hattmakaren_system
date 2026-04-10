using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp1.Views1
{
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        // Denna kod körs när man klickar på knappen
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Vi navigerar till MainPage
            NavigationService.Navigate(new MainPage());
        }
    }
}