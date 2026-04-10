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

        private void BtnAnvandare_Click(object sender, RoutedEventArgs e) { }
        private void BtnKunder_Click(object sender, RoutedEventArgs e) { }
        private void BtnLager_Click(object sender, RoutedEventArgs e) { }
        private void BtnOrder_Click(object sender, RoutedEventArgs e) { }
        private void BtnBestallningar_Click(object sender, RoutedEventArgs e) { }

        private void BtnLoggaUt_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new Uri("Views1/LoginPage.xaml", UriKind.Relative));
            }
        }
    }
}