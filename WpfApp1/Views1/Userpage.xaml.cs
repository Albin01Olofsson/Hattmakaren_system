using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Views1
{
    public partial class AnvandarePage : Page
    {
       
        public AnvandarePage() : this("Admin")
        {
            // Den här säger: "Om ingen säger vem jag är, låtsas att jag är Admin"
        }

        // 2. Den här använder vi när vi faktiskt kör programmet på riktigt
        public AnvandarePage(string userRole)
        {
            InitializeComponent();
            SetupView(userRole);
        }

        private void SetupView(string role)
        {
            // Kontrollera att namnen (AdminPanel, BtnDeleteStaff osv) matchar x:Name i din XAML
            if (role == "Admin")
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
                InputName.Text = "Anna Hattmakare";
                InputEmail.Text = "anna@hatt.se";
            }
        }
    }
}