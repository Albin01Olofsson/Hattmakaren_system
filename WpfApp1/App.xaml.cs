using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Intefaces;
using DAL.Repositorys;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.Views1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = new ServiceCollection();

            // 1. REGISTRERA DATABAS
            services.AddDbContext<DBcontext>();

            // 2. REGISTRERA REPOSITORYS (DAL)
            services.AddScoped<IAnvändarRepo, AnvändarRepo>();
            services.AddScoped<IKundRepo, KundRepo>();      // SAKNADES
            services.AddScoped<IProduktRepo, ProduktRepo>();  // SAKNADES
            services.AddScoped<IOrderRepository, OrderRepo>(); // SAKNADES

            // 3. REGISTRERA TJÄNSTER (BL)
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAnvändarService, AnvändarService>();
            services.AddScoped<IKundService, KundService>();      // SAKNADES
            services.AddScoped<IProduktService, ProduktService>();  // SAKNADES
            services.AddScoped<IOrderService, OrderService>();      // SAKNADES
            services.AddScoped<IPlaneringsYtaService, PlaneringsYtaService>();

            // 4. REGISTRERA VIEWMODELS
            services.AddTransient<LoginViewModel>();
            services.AddTransient<SkapaOrderViewModel>(); // DETTA VAR FELET PÅ BILDEN!

            // 5. BYGG PROVIDER
            ServiceProvider = services.BuildServiceProvider();

            // 6. STARTA APPLIKATIONEN
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Starta med inloggningssidan
            var loginPage = new LoginPage
            {
                DataContext = ServiceProvider.GetRequiredService<LoginViewModel>()
            };

            var loginVm = (LoginViewModel)loginPage.DataContext;
            loginVm.LoginSucceeded += () =>
            {
                // Här navigerar vi till startsida efter lyckad inloggning
                mainWindow.MainFrame.Navigate(new Mainpage());
            };

            mainWindow.MainFrame.Navigate(loginPage);
        }
    }
}
