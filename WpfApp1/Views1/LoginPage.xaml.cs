
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
        private void PassInput_PasswordChanged(object sender, RoutedEventArgs e)// Denna metod körs varje gång användaren ändrar texten i lösenordsfältet
        {
            if (DataContext is LoginViewModel viewModel)// Först kollar vi att DataContext är av typen LoginViewModel, så att vi kan uppdatera Lösenord-egenskapen i vår ViewModel
            {
                viewModel.Lösenord = PassInput.Password;// Sedan sätter vi Lösenord-egenskapen i vår ViewModel till det nya lösenordet som användaren har skrivit in
            }
            PasswordPlaceholder.Visibility =// Slutligen så uppdaterar vi synligheten för vår placeholder-text, så att den bara visas när lösenordsfältet är tomt
                string.IsNullOrWhiteSpace(PassInput.Password)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}