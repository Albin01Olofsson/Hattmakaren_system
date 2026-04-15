using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;

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

        [RelayCommand]
        public void AddAnvändare()
        {
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
