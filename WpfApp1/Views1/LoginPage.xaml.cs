
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
            // Inga fler rader här!
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}