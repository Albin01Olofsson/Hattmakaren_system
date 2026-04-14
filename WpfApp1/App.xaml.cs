using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Intefaces;
using DAL.Repositorys;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
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
        public IServiceProvider ServiceProvider { get; private set; }// Här sätter vi upp vår Dependency Injection-container
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = new ServiceCollection();// Skapa en ny ServiceCollection som kommer att hålla alla våra tjänster

            services.AddDbContext<DBcontext>();// Registrera vår DbContext så att den kan injiceras där den behövs
            services.AddScoped<IAnvändarRepo, AnvändarRepo>();// Registrera våra repositorys och tjänster i DI-containern
            services.AddScoped<IAuthenticationService, AuthenticationService>();// Genom att använda AddScoped så skapas en ny instans av dessa klasser för varje scope (t.ex. varje gång de injiceras i en ViewModel)
            services.AddTransient<LoginViewModel>();// Registrera LoginViewModel som transient eftersom vi vill ha en ny instans varje gång den används (t.ex. varje gång den injiceras i en View)

            var provider = services.BuildServiceProvider();// Bygg vår ServiceProvider som kommer att hantera instansiering av våra klasser och deras beroenden
            ServiceProvider = provider;// Spara ServiceProvider i en egenskap så att vi kan komma åt den senare (t.ex. i våra Views)

            var mainWindow = new MainWindow();// Skapa huvudfönstret för applikationen
            mainWindow.Show();// Visa huvudfönstret

            var loginPage = new LoginPage// Skapa inloggningssidan
            {
                DataContext = provider.GetRequiredService<LoginViewModel>()// Sätt DataContext för inloggningssidan till en instans av LoginViewModel som hämtas från DI-containern
            };


            var loginVm = (LoginViewModel)loginPage.DataContext;

            loginVm.LoginSucceeded += () =>
            {
                mainWindow.MainFrame.Navigate(new Mainpage());
            };
            mainWindow.MainFrame.Navigate(loginPage);

        }
    }
}
