using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System.Text.RegularExpressions;
namespace WpfApp1.ViewModels
{
    public partial class AddAnvändareViewModel : ObservableObject
    {
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
        public void AddAnvändare()
        {
            NamnFel = "";
            EmailFel = "";
            TelefonFel = "";
            LösenordFel = "";
            bool HasErrors = false;
            if(string.IsNullOrWhiteSpace(Namn))
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
                Lösenord = Lösenord,
                IsAdmin = IsAdmin
            };
            AnvändareAdded?.Invoke(nyAnvändare);
        }
    }
}
