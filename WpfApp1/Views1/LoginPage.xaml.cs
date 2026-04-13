
using System;
using System.Windows;
using System.Windows.Controls;
using BL.Services;
using DAL.Repositorys;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
   
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void PassInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Lösenord = PassInput.Password;
            }
            PasswordPlaceholder.Visibility =
                string.IsNullOrWhiteSpace(PassInput.Password)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
        //private void OnLoginSuccess()
        //{
        //    NavigationService.Navigate(new Mainpage());
        //}

        // Denna metod körs när man klickar på "LOGGA IN" i din XAML
        //private void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        // Vi navigerar till dashboarden (Mainpage.xaml)
        //        // Vi använder Uri för att vara säkra på att den hittar rätt i mappen Views1
        //        this.NavigationService.Navigate(new Uri("Views1/Mainpage.xaml", UriKind.Relative));
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ett fel uppstod vid navigering: " + ex.Message);
        //    }
        //}
    }
}