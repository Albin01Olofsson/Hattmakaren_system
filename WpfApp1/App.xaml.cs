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
        private ServiceProvider _provider;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = new ServiceCollection();

            services.AddDbContext<DBcontext>();
            services.AddScoped<IAnvändarRepo, AnvändarRepo>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<LoginViewModel>();

            _provider = services.BuildServiceProvider();

            var mainWindow = new MainWindow();
            mainWindow.Show();

            var loginPage = new LoginPage
            {
                DataContext = _provider.GetRequiredService<LoginViewModel>()
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
