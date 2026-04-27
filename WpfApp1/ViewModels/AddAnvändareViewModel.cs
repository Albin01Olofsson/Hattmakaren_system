using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Security.RightsManagement;
using System.Text.RegularExpressions;
namespace WpfApp1.ViewModels
{
    public partial class AddAnvändareViewModel : ObservableObject
    {
        private readonly IAnvändarService _användarService;
        public AddAnvändareViewModel(IAnvändarService användarService)
        {
            _användarService = användarService;
        }
        public event Action<Användare> AnvändareAdded;

        [ObservableProperty]
        private string namn;
        [ObservableProperty]
        private string telefon;
        [ObservableProperty]
        private string email;
        [ObservableProperty]
        private string lösenord;
        [ObservableProperty]
        private bool isAdmin;
        [ObservableProperty]
        private string namnFel;
        [ObservableProperty]
        private string emailFel;
        [ObservableProperty]
        private string telefonFel;
        [ObservableProperty]
        private string lösenordFel;

        [RelayCommand]
        public async Task AddAnvändare()
        {
            NamnFel = "";
            EmailFel = "";
            TelefonFel = "";
            LösenordFel = "";
            bool HasErrors = false;
            var users = await _användarService.HämtaAllaAnvändare();
            if (string.IsNullOrWhiteSpace(Namn))
            {
                NamnFel = " - obligatorisk!";
                HasErrors = true;
            }

            if (string.IsNullOrWhiteSpace(Telefon))
            {
                TelefonFel = " - obligatorisk!";
                HasErrors = true;
            }
            else if (!Regex.IsMatch(Telefon, @"^[0-9+\s]+$"))
            {
                TelefonFel = " - endast siffror tillåtna!";
                HasErrors = true;
            }
            else if (Telefon.Length < 7)
            {
                TelefonFel = " - för kort nummer!";
                HasErrors = true;
            }else if (!string.IsNullOrWhiteSpace(Telefon))
            {
                foreach(var user in users)
                {
                    if(user.Telefon == Telefon)
                    {
                        TelefonFel = " - finns redan i systemet!";
                        HasErrors = true;
                    }
                }
            }
            
            if (string.IsNullOrWhiteSpace(Email))
            {
                EmailFel = " - obligatorisk!";
                HasErrors = true;
            }
            else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                EmailFel = " - ogiltig e-post!";
                HasErrors = true;
            }else if(!string.IsNullOrWhiteSpace(Email))
            {
                foreach (var user in users)
                {
                    if (user.Email.ToLower() == Email.ToLower())
                    {
                        EmailFel = " - redan finns i systemet!";
                        HasErrors = true;
                    }
                }
            }
            
            if (string.IsNullOrWhiteSpace(Lösenord))
            {
                LösenordFel = " - obligatorisk!";
                HasErrors = true;
            }
            else if (Lösenord.Length < 6)
            {
                LösenordFel = " - måste vara minst 6 tecken!";
                HasErrors = true;
            }

            if (HasErrors)
            {
                return;
            }

            var nyAnvändare = new Användare
            {
                Namn = Namn,
                Telefon = Telefon,
                Email = Email,
                Lösenord = BCrypt.Net.BCrypt.HashPassword(Lösenord),
                IsAdmin = IsAdmin,
                IsActive = true
            };
            AnvändareAdded?.Invoke(nyAnvändare);
            Namn = "";
            Telefon = "";
            Email = "";
            Lösenord = "";
        }

        public void LoadUser(Användare användare)
        {
            if (!användare.IsAdmin)
            {
                Namn = användare.Namn;
                Telefon = användare.Telefon;
                Email = användare.Email;
            }
            
        }
    }
}
