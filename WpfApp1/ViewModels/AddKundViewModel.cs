using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;

namespace WpfApp1.ViewModels
{
    public partial class AddKundViewModel : ObservableObject
    {
        public event Action<Kund> KundAdded;

        [ObservableProperty]
        private string namn;

        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string telefon;

        [ObservableProperty]
        private string adress;
        [ObservableProperty]
        private string land;
        [ObservableProperty]
        private string stad;



        [RelayCommand]
        private void AddKund()
        {
            var nyKund = new Kund
            {
                Namn = Namn,
                Email = Email,
                Telefon = Telefon,
                Adress = Adress,
                Land = Land,
                Stad = Stad
            };
            KundAdded?.Invoke(nyKund);
        }
    }

}
