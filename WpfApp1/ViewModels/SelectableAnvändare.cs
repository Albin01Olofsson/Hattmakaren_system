using CommunityToolkit.Mvvm.ComponentModel;
using Models;

namespace WpfApp1.ViewModels
{
    public partial class SelectableAnvändare : ObservableObject
    {
        public Användare User { get; set; }

        [ObservableProperty]
        private bool isSelected;
    }
}
