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
    public partial class LoginViewModel: ObservableObject
    {
        private readonly IAuthenticationService _authService;

        public LoginViewModel(IAuthenticationService authService)
        {
            _authService = authService;
        } 

        public event Action LoginSucceeded;

        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string lösenord;

        [RelayCommand]
        private void Login()
        {
            var result = _authService.Login(Email, Lösenord);
            if (result)
            {
                LoginSucceeded?.Invoke();
            }
        }

    }
}
