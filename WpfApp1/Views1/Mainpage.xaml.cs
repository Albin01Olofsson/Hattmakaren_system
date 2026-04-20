using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Repositorys;
using System;
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

            // Initiera tjänster och databas
            OrderRepo repo = new OrderRepo(new DBcontext());
            _orderService = new OrderService(repo);
            vm = new OrderVM(_orderService);

            // Körs när sidan laddas
            LoadUser();
        }

        /// <summary>
        /// Uppdaterar profilfältet längst ner i sidomenyn.
        /// </summary>
        public void LoadUser()
        {
            var användare = Session.CurrentUser;

            // Kontrollera att elementen finns (viktigt för Blend)
            if (UserNameText == null || RoleText == null) return;

            if (användare == null)
            {
                UserNameText.Text = "Gäst";
                RoleText.Text = "Ej inloggad";
                return;
            }

            UserNameText.Text = användare.Namn;
            RoleText.Text = användare.IsAdmin ? "Administratör" : "Anställd";
        }

        // --- NAVIGERINGSKOD ---

        private void PrepareNavigation()
        {
            // Döljer startsidans statistik-kort så att Frame-innehållet syns
            if (DashboardStartView != null)
            {
                DashboardStartView.Visibility = Visibility.Collapsed;
            }
        }

        private void BtnAnvandare_Click(object sender, RoutedEventArgs e)
        {
            PrepareNavigation();
            MainFrame.Navigate(new AnvandarePage());
        }

        private void BtnKunder_Click(object sender, RoutedEventArgs e)
        {
            PrepareNavigation();
            MainFrame.Navigate(new KunderPage());
        }

        private void BtnLager_Click(object sender, RoutedEventArgs e)
        {
            PrepareNavigation();
            // MainFrame.Navigate(new LagerPage()); // Lägg till när sidan finns
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            PrepareNavigation();
            MainFrame.Navigate(new OrderPage(new OrderVM(_orderService)));
        }

        private void BtnBestallningar_Click(object sender, RoutedEventArgs e)
        {
            PrepareNavigation();
            MainFrame.Navigate(new BestallningarPage());
        }

        private void BtnSchema_Click(object sender, RoutedEventArgs e)
        {
            PrepareNavigation();
            MainFrame.Navigate(new AnvPage());
        }

        private void BtnLoggaUt_Click(object sender, RoutedEventArgs e)
        {
            // 1. Rensa sessionen
            Session.CurrentUser = null;

            // 2. Hämta tjänster för inloggningssidan
            var app = (App)Application.Current;
            var loginVm = (LoginViewModel)app.ServiceProvider.GetService(typeof(LoginViewModel));

            var loginPage = new LoginPage
            {
                DataContext = loginVm
            };

            // 3. Hantera vad som händer efter lyckad inloggning igen
            loginVm.LoginSucceeded += () =>
            {
                if (Application.Current.MainWindow is MainWindow mw)
                {
                    mw.MainFrame.Navigate(new Mainpage());
                }
            };

            // 4. Navigera bort
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(loginPage);
            }
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // Här kan man lägga logik som ska köras varje gång man byter sida i framen
        }
    }
}