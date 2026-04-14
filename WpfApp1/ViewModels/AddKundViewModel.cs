using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public partial class AddKundViewModel: ObservableObject
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

        [RelayCommand]
        private void AddKund()
        {
            var nyKund = new Kund
            {
                Namn = Namn,
                Email = Email,
                Telefon = Telefon,
                Adress = Adress
            };
            KundAdded?.Invoke(nyKund);
        }
    }
}
