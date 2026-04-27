using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Repositorys;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;
using WpfApp1.Views1.Planneringsviews;


namespace WpfApp1.Views1
{
    public partial class Mainpage : Page
    {
        public OrderVM vm { get; set; }

        private IOrderService _orderService;
        public Mainpage()
        {
            InitializeComponent();
            LoadUser();
            var context = new DBcontext();
            OrderRepo repo = new OrderRepo(context);
            _orderService = new OrderService(repo, context);
            vm = new OrderVM(_orderService);
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
            try
            {
                DashboardStartView.Visibility = Visibility.Collapsed;
                MainFrame.Visibility = Visibility.Visible;
                MainFrame.Navigate(new LagerPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "Fel när Lager öppnas",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            // Här kan du lägga till navigering för Order senare
            MainFrame.Navigate(new OrderPage(new OrderVM(_orderService)));
        }

        private void BtnBestallningar_Click(object sender, RoutedEventArgs e)
        {
            DashboardStartView.Visibility = Visibility.Collapsed;
            MainFrame.Navigate(new BestallningarPage());
        }

        private void BtnStatistik_Click(object sender, RoutedEventArgs e)
        {
            DashboardStartView.Visibility = Visibility.Collapsed;
            MainFrame.Navigate(new StatistikPage());
        }

        private void BtnReklamation_Click(object sender, RoutedEventArgs e)
        {
            DashboardStartView.Visibility = Visibility.Collapsed;
            MainFrame.Navigate(new ReklamationPage());
        }

        private void BtnSchema_Click(object sender, RoutedEventArgs e)
        {
            // Vi säger åt vår Frame att visa AnvPage
            MainFrame.Navigate(new AnvPage());
        }

        private void BtnFörfrågningar_Click(object sender, RoutedEventArgs e)
        {
            var mailRepo = new MailRepository();
            MainFrame.Navigate(new FörfrågningarPage(new FörfrågningVM(new MailService(mailRepo))));
        }

        private void BtnLeveranser_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SpårningPage());
        }


        public Frame GetFrame()
        {
            return MainFrame;
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

        public void LoadUser()
        {
            var användare = Session.CurrentUser;
            if (användare == null)
            {
                UserNameText.Text = "Välkommen, gäst!";
                RoleText.Text = "Du är inte inloggad.";
                return;
            }
            UserNameText.Text = användare.Namn;
            RoleText.Text = användare.IsAdmin ? "Administratör" : "Anställd";
        }
    }
}
