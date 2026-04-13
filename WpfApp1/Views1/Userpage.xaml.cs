using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Views1
{
    public partial class AnvandarePage : Page
    {
       
        public AnvandarePage()
        {
            InitializeComponent();
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
                AdminPanel.Visibility = Visibility.Visible;
                BtnDeleteStaff.Visibility = Visibility.Visible;
                BtnDeleteCustomer.Visibility = Visibility.Visible;
                TxtHeaderTitle.Text = "SYSTEMADMINISTRATION";
                TxtFormTitle.Text = "REGISTRERA KONTO";
            }
            else
            {
                AdminPanel.Visibility = Visibility.Collapsed;
                BtnDeleteStaff.Visibility = Visibility.Collapsed;
                BtnDeleteCustomer.Visibility = Visibility.Collapsed;

                TxtHeaderTitle.Text = "MINA INSTÄLLNINGAR";
                TxtHeaderSub.Text = "Här kan du hantera din profil och se kundregistret";
                TxtFormTitle.Text = "DIN PROFIL";

                // Här fyller vi i testdata så länge
                InputName.Text = användare.Namn;
                InputEmail.Text = användare.Email;
            }
        }
    }
}