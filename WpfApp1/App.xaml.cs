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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var services = new ServiceCollection();

            services.AddDbContext<DBcontext>();
            services.AddScoped<IAnvändarRepo, AnvändarRepo>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<LoginViewModel>();

            ServiceProvider = services.BuildServiceProvider();

            var loginPage = new Views1.LoginPage
            {
                DataContext = ServiceProvider.GetService<LoginViewModel>()
            };
            loginPage.Show();

        }

        
    }

}
