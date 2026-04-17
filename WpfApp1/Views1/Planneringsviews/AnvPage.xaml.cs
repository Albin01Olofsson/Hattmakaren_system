using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Views1.Planneringsviews
{
    /// <summary>
    /// Interaction logic for AnvPage.xaml
    /// </summary>
    public partial class AnvPage : Page
    {
        public AnvPage()
        {
            InitializeComponent();
            this.DataContext = new WpfApp1.ViewModels.AnvPlanViewModel();


        }
        private void Ordrar_Click(object sender, RoutedEventArgs e)
        {
            // Vi skapar en ny instans av fönstret
            SkapaAktivitet skapaFönster = new SkapaAktivitet();

            // ShowDialog() gör att man måste stänga fönstret innan man kan trycka på schemat igen
            // Show() låter båda fönstren vara öppna samtidigt
            skapaFönster.ShowDialog();
        }
    }
}
