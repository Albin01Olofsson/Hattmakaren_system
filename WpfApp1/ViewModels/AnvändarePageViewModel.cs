using BL.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace WpfApp1.ViewModels
{
    public partial class AnvändarePageViewModel : ObservableObject
    {
        private readonly IAnvändarService _användarService;

        public AddAnvändareViewModel AddAnvändareVM { get; set; }
        public AnvändareViewModel ListaVM { get; set; }
        
        [ObservableProperty]
        private string formTitle;
        [ObservableProperty]
        private bool canEditUsers;

        public AnvändarePageViewModel(IAnvändarService användarService)
        {
            _användarService = användarService;

            AddAnvändareVM = new AddAnvändareViewModel(_användarService);
            ListaVM = new AnvändareViewModel(_användarService);

            AddAnvändareVM.AnvändareAdded += OnUserAdded;

            var user = Session.CurrentUser;
            AddAnvändareVM.LoadUser(user);
            CanEditUsers = user?.IsAdmin == true;

            FormTitle = CanEditUsers
                ? "REGISTRERA KONTO"
                : "DINPROFIL";
        }

        private async void OnUserAdded(Användare användare)
        {
            await _användarService.LäggTillAnvändare(användare);
            await ListaVM.Reload();
        }
    }
}
