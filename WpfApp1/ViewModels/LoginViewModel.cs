using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BL.Interfaces;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp1.ViewModels
{
    public partial class LoginViewModel: ObservableObject// Vi ärver från ObservableObject så att vi kan använda [ObservableProperty] och [RelayCommand]
    {
        private readonly IAuthenticationService _authService;// Vi injicerar vår AuthenticationService så att vi kan använda den för att logga in användaren

        public LoginViewModel(IAuthenticationService authService)
        {
            _authService = authService;
        } 

        public event Action LoginSucceeded;// En händelse som vi kommer att utlösa när inloggningen lyckas, så att vår View kan reagera på det (t.ex. navigera till dashboarden)

        [ObservableProperty]// Med hjälp av [ObservableProperty] så skapas både en privat fält och en publik egenskap för Email och Lösenord, samt att PropertyChanged eventet utlöses när de ändras
        private string email;
        [ObservableProperty]// Samma sak här för Lösenord
        private string lösenord;
        [ObservableProperty]
        private string loginFel;

        [RelayCommand]// Med hjälp av [RelayCommand] så skapas en ICommand egenskap som vi kan binda till i vår View, och när den kommandot körs så kommer Login() metoden att anropas
        private async Task Login()
        {
            LoginFel = "";
            if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Lösenord))
            {
                LoginFel = "Fyll i alla fält!";
                return; 
            }

            var användare = await _authService.Login(Email, Lösenord);
            if (användare != null)
            {
                Session.CurrentUser = användare;
                LoginSucceeded?.Invoke();
            }
            else
            {
                LoginFel = "Fel email eller lösenord!";
                return;
            }
        }

    }
}
