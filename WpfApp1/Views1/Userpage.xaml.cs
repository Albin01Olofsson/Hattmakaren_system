using BL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    public partial class AnvandarePage : Page
    {
        public AnvandarePage()
        {
            InitializeComponent();
            var serviceProvider = ((App)Application.Current).ServiceProvider;
            var service = serviceProvider.GetRequiredService<IAnvändarService>();
            
            DataContext = new AnvändarePageViewModel(service);
            SetupView();
        }

        private void SetupView()
        {
            var användare = Session.CurrentUser;
            if (användare.IsAdmin)
            {
                AdminPanel.Visibility = Visibility.Visible;
            }
            else
            {
                AdminPanel.Visibility = Visibility.Visible;
                TxtHeaderTitle.Text = "MIN PROFIL";
                
                //InputName.Text = användare.Namn;
                //InputTelefon.Text = användare.Telefon;
                //InputEmail.Text = användare.Email;

                InputName.IsReadOnly = true;
                InputTelefon.IsReadOnly = true;
                InputEmail.IsReadOnly = true;
            }
        }
    }
}