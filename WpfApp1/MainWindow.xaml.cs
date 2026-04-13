using BL.Services;
using DAL;
using DAL.Repositorys;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.ViewModels;
using WpfApp1.Views1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Skrivet här för att jag bypassar MainPage
            OrderRepo repo = new OrderRepo(new DBcontext());
            OrderService service = new OrderService(repo);
            OrderVM vm = new OrderVM(service);
            Content = new OrderPage(vm);
        }
    }
}