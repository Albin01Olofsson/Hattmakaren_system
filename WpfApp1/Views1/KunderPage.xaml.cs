using BL.Services;
using DAL;
using DAL.Repositorys;
using System.Windows.Controls;
using WpfApp1.ViewModels;

namespace WpfApp1.Views1
{
    /// <summary>
    /// Interaction logic for KunderPage.xaml
    /// </summary>
    public partial class KunderPage : Page
    {
        public KunderPage()
        {
            InitializeComponent();

            DataContext = new KundVM(new KundService(new KundRepo(new DBcontext())));
        }
    }
}
