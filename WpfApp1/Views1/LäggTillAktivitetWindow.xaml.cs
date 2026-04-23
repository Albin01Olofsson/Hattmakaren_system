using Models;
using System.Windows;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for LäggTillAktivitetWindow.xaml
    /// </summary>
    public partial class LäggTillAktivitetWindow : Window
    {
        public LäggTillAktivitetWindow()
        {
            InitializeComponent();
            DeltagarListBox.SelectionChanged += (s, e) =>
            {
                if (DataContext is LäggTillAktivitetViewModel vm)
                {
                    vm.UppdateraValdaDeltagare(DeltagarListBox.SelectedItems);
                }
            };
        }
    }
}
