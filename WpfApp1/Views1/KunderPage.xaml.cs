using BL.Interfaces;
using BL.Services;
using DAL;
using DAL.Repositorys;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.ViewModels;


namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for KunderPage.xaml
    /// </summary>
    public partial class KunderPage : Page
    {
        private readonly IKundService _kundService;
        public KunderPage()
        {
            InitializeComponent();

            _kundService = new KundService(new KundRepo(new DBcontext()));
            DataContext = new KundVM(_kundService);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var window = new AddKundWindow();
            if (window.ShowDialog() == true)
            {
                _kundService.AddKund(window.CreatedKund);
                var vm = (KundVM)this.DataContext;
                vm.Kunder.Add(window.CreatedKund);
                MessageBox.Show("Kund tillagd!");//hej
            }
        }


    }
}
